shader_type canvas_item;

uniform vec2 resolution = vec2(600.0, 400.0);
uniform vec3 line_color: source_color = vec3(0.2, 0.3, 0.5);
//uniform float zoom: hint_range(1.0, 50.0, 0.1) = 20.0;
uniform float thickness: hint_range(0.1, 10.0, 0.1) = 2.0;
uniform float brightness: hint_range(0.1, 4.0, 0.1) = 2.0;
uniform int cols = 12;
uniform int rows = 8;

void fragment() {
	// Called for every pixel the material is visible on.
}
