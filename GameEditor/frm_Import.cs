using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameEditor
{
    public partial class frm_Import : Form
    {
        public frm_Import()
        {
            InitializeComponent();
        }

        private void button_Import_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK) {
                
                //create file header
                string fileContent = @"#------------------------------ Global Properties ------------------------------#

/outputDir:bin
/intermediateDir:obj
/platform:Windows
/config:
/profile:HiDef
/compress:False

#------------------------------ References ------------------------------#
";
                string outputDir = Path.Combine(Application.StartupPath, "SourceAssets");
                Directory.CreateDirectory(Path.Combine(Application.StartupPath, "SourceAssets"));

                for (var i = 0; i < openFileDialog1.FileNames.Length; i++) {
                    File.Delete(Path.Combine(outputDir, openFileDialog1.SafeFileNames[i]));
                    File.Copy(openFileDialog1.FileNames[i], Path.Combine(outputDir, openFileDialog1.SafeFileNames[i]));
                    string newEntry = PrepareEntry(openFileDialog1.SafeFileNames[i], Path.GetExtension(openFileDialog1.SafeFileNames[i]));
                    fileContent += newEntry;
                }

                textBox_MGCBContent.Text = fileContent;
                var outputMGCBFilename = Path.Combine(Application.StartupPath, @"SourceAssets\ContentToBuild.mgcb");
                var fullPathToMGCBExecutable = @"c:\Program Files (x86)/MSBuild/MonoGame/v3.0/Tools/MGCB.exe";
                var platform = "/platform:Windows";

                // Save the .mgcb file
                File.WriteAllText(outputMGCBFilename, fileContent);

                // Create Batch File
                var outputBatchContent = "cmd /c \"\"" + fullPathToMGCBExecutable + "\" " + platform + " /@:\"" + outputMGCBFilename + "\"";
                var outputBatchFileLocation = Path.Combine(Application.StartupPath, @"SourceAssets\buildcontent.bat");
                File.WriteAllText(outputBatchFileLocation, outputBatchContent);

                // call batch file
                using (var proc = new Process()) {
                    proc.StartInfo.FileName = outputBatchFileLocation;
                    proc.StartInfo.WorkingDirectory = Path.GetDirectoryName(outputMGCBFilename);
                    proc.StartInfo.RedirectStandardOutput = true;
                    proc.StartInfo.UseShellExecute = false;
                    proc.Start();
                    var output = proc.StandardOutput.ReadToEnd();
                    proc.WaitForExit();
                }

                var fileList = Directory.GetFiles(Path.Combine(Application.StartupPath, @"SourceAssets\bin"));
                var targetDirectory = Path.Combine(Application.StartupPath, "Content");
                foreach (var t in fileList) {
                    var targetPlusFilename = Path.Combine(targetDirectory, Path.GetFileName(t));
                    File.Copy(t, targetPlusFilename, true);
                }

                MessageBox.Show("Import Complete");

            }
        }

        public string PrepareEntry(string fileName, string fileExtension) {
            var output = "#begin " + fileName;
            fileExtension = fileExtension.ToLower();
            switch (fileExtension) {
                case ".fbx": {
                    output += $@"{Environment.NewLine}/importer:FbxImporter
/processor:ModelProcessor
/processorParam:ColorKeyColor=0,0,0,0
/processorParam:ColorKeyEnabled=True
/processorParam:DefaultEffect=BasicEffect
/processorParam:GenerateMipmaps=True
/processorParam:GenerateTangentFrames=False
/processorParam:PremultiplyTextureAlpha=True
/processorParam:PremultiplyVertexColors=True
/processorParam:ResizeTexturesToPowerOfTwo=False
/processorParam:RotationX=0
/processorParam:RotationY=0
/processorParam:RotationZ=0
/processorParam:Scale=1
/processorParam:SwapWindingOrder=False
/processorParam:TextureFormat=DxtCompressed
/build:{fileName}";
                }
                    break;
                case ".png": {
                    output += $@"{Environment.NewLine}/importer:TextureImporter
/processor:TextureProcessor
/processorParam:ColorKeyColor=255,0,255,255
/processorParam:ColorKeyEnabled=True
/processorParam:GenerateMipmaps=False
/processorParam:PremultiplyAlpha=True
/processorParam:ResizeToPowerOfTwo=False
/processorParam:TextureFormat=Color
/build:{fileName}";
                }
                    break;
                default: {
                    //something else, skip it
                }
                    break;
            }

            output += Environment.NewLine + Environment.NewLine;
            return output;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
