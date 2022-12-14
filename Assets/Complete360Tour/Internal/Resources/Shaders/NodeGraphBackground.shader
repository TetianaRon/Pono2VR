Shader  "Hidden/DigitalSalmon/C360/NodeGraphBackground" {
    Properties {
		_Foreground("Foreground", Color) = (0,0,0,0)
		_Background("Background", Color) = (0,0,0,0)
		_Rect("Rect", Vector) = (0,0,0,0)
    }
    SubShader {
        Tags {

        }
        Pass {
            Name "FORWARD"

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
			
            #include "UnityCG.cginc"
		
			uniform float4 _Foreground;
			uniform float4 _Background;
			uniform float4 _Rect;
			uniform float _Zoom;          
            
            float GridField(float2 uv, float smoothing, int steps, float thickness, float brightnessExpo){
				smoothing *= 0.001;
				smoothing /= _Zoom;
				thickness *= 0.001;
				thickness *= _Zoom;
				float val = 0;
				steps += 1;
				for (int i = 1; i < steps; i++)
				{
					float uvTile = pow(2, i) / 2;
					float2 tiledUV = frac(uv * uvTile);
					float fixThickness = thickness * uvTile;// *pow(uvTile, thicknessExpo) / 2;
					float fixSmoothing = smoothing * uvTile;// *pow(uvTile, thicknessExpo) / 2;

					float xLow = smoothstep(0 + fixThickness  + fixSmoothing, 0 + fixThickness, tiledUV.x);
					float xHigh = smoothstep(1 - fixThickness  - fixSmoothing, 1 - fixThickness, tiledUV.x);

					float yLow = smoothstep(0 + fixThickness  + fixSmoothing, 0 + fixThickness, tiledUV.y);
					float yHigh = smoothstep(1 - fixThickness  - fixSmoothing, 1 - fixThickness, tiledUV.y);

					float brightnessMul = pow(1 - ((float)i / steps), brightnessExpo);
					float contribution = max(max(xLow, xHigh), max(yLow, yHigh)) *brightnessMul;

					val = max(val, contribution);
				}


				return val;
            }

			float sampleBand(float field, float thickness, float smoothing){
				if (smoothing == 0){
					return field < thickness && field > -thickness;
				}
				return min(smoothstep(thickness + smoothing, thickness, field), smoothstep(-thickness - smoothing, -thickness, field));
			}

			float CardinalField(float2 uv, float thickness, float aspect)
			{
				float2 cardinalUv = uv;


				thickness *= 0.01;
				thickness *= _Zoom;

				float smoothing = 1.0/120;
				smoothing /= _Zoom;

				return max(sampleBand(cardinalUv.x, thickness, smoothing),sampleBand(cardinalUv.y, thickness , smoothing));
			}

			
            
            struct VertexInput {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv = v.uv;
				o.uv.y = 1-o.uv.y;
				o.pos = UnityObjectToClipPos(v.vertex); 
                return o;
            }

            float4 frag(VertexOutput i) : COLOR 
			{

				// _Rect.xy = Window Size
				// _Rect.zw = Global Offset
		
				float2 uv = float2(((i.uv.x * _Rect.x) - _Rect.z), ((i.uv.y * _Rect.y) - _Rect.w));

				float aspect = _Rect.x / _Rect.y;
				uv /= 120;
				
				float cardinal = CardinalField(uv, 1, aspect);
				float gridField = GridField(frac(uv), 5, 3, 1, 1.5); 

				float4 gridColor = lerp(_Background, _Foreground , gridField);
				float4 cardinalGridColor = lerp(gridColor, _Foreground * 1.2, cardinal);

				// Vignette

				const float vignetteDistance = 0.075;
				float vignetteField = min(min(smoothstep(0,(vignetteDistance/aspect),i.uv.x), smoothstep(1,1-(vignetteDistance/aspect),i.uv.x)), min(smoothstep(0,vignetteDistance,i.uv.y), smoothstep(1,1-vignetteDistance,i.uv.y)));
				vignetteField = lerp(0.8,1,vignetteField);

				return cardinalGridColor * vignetteField;
            }
            ENDCG
        }
    }
}
