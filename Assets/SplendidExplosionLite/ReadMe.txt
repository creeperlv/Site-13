
Thank you for using our asset.
Full version of Spledid Explosion from here:
https://assetstore.unity.com/packages/vfx/particles/fire-explosions/splendid-explosion-and-smoke-effect-153321

There are following 1 prefab folders.
"Explosion" folder:

Just simply drag prefab in 2 prefab folders into your scene.

Instruction for "Scene1"
"Scene1" demonstrates every effect repeatedly.
Looping option of the particle objects in inspector in "Scene1" are checked because to demonstrate repeated effect. 
But Looping option of the particle objects in "Prefab" folders are not checked. Because they will explode once in real game.

Order in layer
Usually every particle system object's "order in layer" option in Renderer component is zero. But some background particle system(for example background smoke)'s "order in layer" option is -1. Because more small digit is located more back side of the objects.
All particle system has basically "0" or "-1" for "order in layer" option.

How to resize prefab:
1. Click an explosion game object from Hierarchy.
2. Expand the game object.
3. Select all child objects by shift + click. But deselect parent object.
4. Change scale of X, Y, Z axis on Transform component by inspector

How to remove full version promotion:
Find folder "SplendidExplosionLite/Editor" and remove "Editpr" folder. Then promotion windows will not appear.

technical support:
oharinth@gmail.com

