Asset Creator - Vladyslav Horobets (Hovl).
-----------------------------------------------------

If you want to use post-effects like in the demo video:
https://youtu.be/hZSZ2Q8MF3k

1) Shaders
1.1)The "Use depth" on the material from the custom shaders is the Soft Particle Factor.
1.2)Use "Center glow"[MaterialToggle] only with particle system. This option is used to darken the main texture with a white texture (white is visible, black is invisible).
    If you turn on this feature, you need to use "Custom vertex stream" (Uv0.Custom.xy) in tab "Render". And don't forget to use "Custom data" parameters in your PS.
1.3)The distortion shader only works with standard rendering. Delete (if exist) distortion particles from effects if you use LWRP or HDRP!
1.4)You can change the cutoff in all shaders (except Add_CenterGlow and Blend_CenterGlow ) using (Uv0.Custom.xy) in particle system.

  SUPPORT ASSET FOR URP or HDRP here --> Hovl Studio -> Epic toon VFX 2 -> URP and HDRP patches folder

Contact me if you have any questions.
My email: hovlstudi1@gmail.com