# Added Features

Keybinds can be accessed on the **Help** menu.

Middle mouse navigates the scene.

right click resets zoom.

Selection happens outside of select mode by design.

All Selection is z-depth based, and respect current selections if partially under another object.

## Property Grid

Assets implement `INotifyPropertyChanged` on many properties, and so updates to the grid or the editor space happen in sync.

### Texture Name

Texture name can be changed **only** to the name of another texture in the project, allowing for a sprite swap.

### Entity Name

Entities have names allocatable in the property grid which is reflected in the `TreeView`.

### Layers

A layer based sorting method has been added, **Background** layer items draw underneath **Foreground**. Sorting within a layer can be achieved with the **Sorting Fudge** property in the property grid. 

### Scale

You can scale an object with the scale tool or using the property grid.

Height/Width properties are tied to the Scale property in that width/height are a factor of Scale*Texture(width/height). Modifying any of those 3 properties will update the others to reflect the changes.

### Rotation

Rotation around the objects origin can be achieved by modifying the rotation in the property grid.

## Deletion

hitting the *Delete* key at any time will remove the currently selected item.

## Hierarchy

Selection in the hierarchy will select an object in the editor window.

The `TreeView` provides for nested (parent/child) hierarchies available through drag/drop

Children will move/scale/delete with their parents.

## Asset list

The asset list in the bottom right allows for the removal of an imported asset from the editor.

## Zoom slider

As well as scroll wheel zooming, there is slider provided in the editor for convenience.

