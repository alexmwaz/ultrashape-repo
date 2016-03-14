Shader "Custom/InvisibleMaskA" {
  SubShader {
    // draw after all opaque objects (queue = 2002):
    Tags { "Queue"="Geometry+2" }
    Pass {
      Blend Zero One // keep the image behind it
    }
  } 
}