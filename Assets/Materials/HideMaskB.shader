Shader "Custom/InvisibleMaskB" {
  SubShader {
    // draw after all opaque objects (queue = 2001):
    Tags { "Queue"="Geometry+3" }
    Pass {
      Blend Zero One // keep the image behind it
    }
  } 
}