How to use it
1、Add a Projector component to a empty gameobject.
2、Set the "Shaders/Caustics_prejector.shader" to the variable "Material" of Projector.
3、Add the "Scripts/CausticFrameCtr.cs" to the MainCamera.
4、Set the Projector to the variable "Projector" of CausticFrameCtr.

About shader
1、The "Caustic_projector.shader" is used for opaque gameobject.
2、The "Caustic_transform.shader" is used for transparent gameobject.

Notice
1、The texture "caustics_48x512" is not suported mipmap setting, or not it will be a gap when rendering.
2、"UnderwaterCaustic_4.0.unitypackage" is suported Unity4.0 or highter.
