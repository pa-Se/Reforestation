Shader "Custom/Plant Stem" {
//Initialisieren des Shader-Materials: abhängig vom Material
Properties { 
    _MainTex ("Base (RGB)", 2D) = "white" {}
    _Color ("Color", Color) = (1,1,1,1)
    _GrowthPercent ("Growth Percent", Range(0, 1)) = 0
}
SubShader {

    //Optimierung, welche Polygone nicht rendert, wenn sie vom Spieler wegzeigen/versteckt sind durch Verdeckung
    Cull Off

    //Tags { "Queue" = "Geometry" "IgnoreProjector" = "True"  "RenderType"="Geometry" }
    //LOD 100
    

    //Definiere Shader als Surface-Shader -> interagiert mit Licht
    CGPROGRAM
    #pragma surface surf Standard addshadow

    // shader model 3.0 
    #pragma target 3.0
    sampler2D _MainTex;
    
    //zugewiesene Textur vom Material
    struct Input {
        float2 uv_MainTex;
    };
    

    fixed4 _Color;
    float _GrowthPercent;

    //Surface Function _ IN = Structure Input

    void surf (Input IN, inout SurfaceOutputStandard o) {
      
        //Farbgebung Textur
        fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
        o.Albedo = _Color;
        o.Metallic = 0;
        o.Smoothness = 0;
        o.Alpha = c.a;
        
        // Growth_Percent wird stets größer durch Wachstum in Plant.cs --> plantHidden wird immer kleiner --> es wird immer weniger Textur verborgen
        float plantSize = 1-saturate(_GrowthPercent); //saturate skaliert input (growthPercent) auf einen Bereich 0-1
        clip(IN.uv_MainTex.x - plantSize); // clip verwirft das aktuelle Pixel bei Wert <0, d.h. wird nicht gerendert
    }
ENDCG
}
FallBack "VertexLit"
}