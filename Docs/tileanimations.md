# Basic Tile Setup #

## TileBase Class ##
Each Tile in a scene must utilise the TileBase class.
Alternatively, it can use a script which inherits from the TileBase.

## Editable Fields in the GUI ##
The TileBase class includes a number of editable fields in the Unity GUI. Many fields titles can be hovered over to see a tooltip description. These fields are made available for customisation and testing without the need to edit the TileBase code.

![TileBase Tooltips](images/tile_transition-overides_tooltip-example.JPG)

## Tile Type Tag ##
Each tile must also be tagged with it's tile type. Types that the code will recognise are shown here:

![Tile Tag](images/tile_setup_tag.JPG)

## Animation Controller ##
Each tile must also be given an animation controller. The animation controller must be set to **_Apply Root Motion_**

![Tile Tag](images/tile_setup_animator.JPG)



# Setting Up the Animation Controller #

![Animation Clip Nodes](images/tile-animator_setup_node-example.JPG)

![Animation Clip Node Settings](images/tile-animator_setup_node-settings-example.JPG)



# Specifying Tile Animation Types to Use#

## Per Tile Type ##

![Tile Animations Per Type](images/tile_transition-overides-example.JPG)

## Per Scene ##

![Tile Animations Per Scene](images/map-transition_settings.JPG)






