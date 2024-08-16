.GMS 1.00

BlindData "BlindData" 200 1953719668 1635017060 0

Model "root_Model" {
	BoundingBox -260.000061 -160.000076 41.452126 260.000061 160.000076 41.452248
	Bone "root_Bone" {
		Translate 0.000000 0.000000 0.000000
		RotateZYX 0.000000 0.000000 3.141593
		Scale 1.000000 1.000000 1.000000
	}
	Bone "parts_top_Bone" {
		ParentBone "root_Bone"
		Translate 0.000000 0.000000 0.000000
		RotateZYX 0.000000 0.000000 0.000000
		Scale 1.000000 1.000000 1.000000
	}
	Bone "back_Bone" {
		BoundingBox -260.000000 0.000000 -160.000000 260.000000 0.000000 160.000000
		ParentBone "parts_top_Bone"
		Translate 0.000000 -0.000002 41.452187
		RotateZYX 1.570796 0.000000 0.000000
		Scale 1.000000 1.000000 1.000000
		DrawPart "back_0_Part"
	}
	Part "back_0_Part" {
		BoundingBox -260.000000 0.000000 -160.000000 260.000000 0.000000 160.000000
		Mesh "back_0_Mesh" {
			SetMaterial "root_Model_0_Material"
			DrawArrays "back_0_Arrays" TRIANGLES 6 1 0 1 2 3 4 5
		}
		Arrays "back_0_Arrays" VERTEX|NORMAL 6 1 0 {
			-260.000000 0.000000 159.998169 0.000000 -1.000000 0.000000
			-260.000000 0.000000 -160.000000 0.000000 -1.000000 0.000000
			260.000000 0.000000 159.998169 0.000000 -1.000000 0.000000
			-260.000000 0.000000 -160.000000 0.000000 -1.000000 0.000000
			260.000000 0.000000 -160.000000 0.000000 -1.000000 0.000000
			260.000000 0.000000 159.998169 0.000000 -1.000000 0.000000
		}
	}
	Material "root_Model_0_Material" {
		Diffuse 0.999999 0.000000 0.000000 0.000000
		Ambient 0.999999 0.000000 0.000000 0.000000
		BlindData "transAlgo" 1
	}
	Motion "root_Model_Motion" {
		FrameLoop 0.000000 80.000000
		FrameRate 30.000000
		Animate "Bone::root_Bone" Translate 0 "root_Bone_trs_FCurve"
		Animate "Bone::root_Bone" RotateQ 0 "root_Bone_rot_FCurve"
		Animate "Bone::parts_top_Bone" Translate 0 "parts_top_Bone_trs_FCurve"
		Animate "Bone::back_Bone" Translate 0 "back_Bone_trs_FCurve"
		Animate "Bone::back_Bone" RotateQ 0 "back_Bone_rot_FCurve"
		Animate "Material::root_Model_0_Material" Diffuse 0 "root_Model_0_Material_Mat_Motion_FCurve"
		FCurve "root_Bone_trs_FCurve" CONSTANT 3 1 0 {
			0.000000 0.000000 0.000000 0.000000
		}
		FCurve "root_Bone_rot_FCurve" CONSTANT 4 1 0 {
			0.000000 0.000000 0.000000 -1.000000 0.000000
		}
		FCurve "parts_top_Bone_trs_FCurve" LINEAR 3 3 0 {
			0.000000 0.000000 0.000000 0.000000
			67.000000 0.000000 0.000000 0.000000
			68.000000 0.000000 508.250000 0.000000
		}
		FCurve "back_Bone_trs_FCurve" CONSTANT 3 1 0 {
			0.000000 0.000000 0.000000 41.437500
		}
		FCurve "back_Bone_rot_FCurve" CONSTANT 4 1 0 {
			0.000000 0.707031 0.000000 0.000000 0.707031
		}
		FCurve "root_Model_0_Material_Mat_Motion_FCurve" LINEAR 4 2 0 {
			0.000000 0.235229 0.329346 0.482178 0.000000
			5.000000 0.235229 0.329346 0.482178 1.000000
		}
	}
}
