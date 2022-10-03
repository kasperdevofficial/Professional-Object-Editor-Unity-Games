using System;
using System.Reflection;
using UnityEngine;

// Token: 0x02000243 RID: 579
public class ObjEdit : MonoBehaviour
{
	// Token: 0x06000B60 RID: 2912 RVA: 0x00045A60 File Offset: 0x00043C60
	private void AddChild(GameObject Obj)
	{
		foreach (object obj in Obj.transform)
		{
			Transform transform = (Transform)obj;
			if (transform.gameObject != this.selectedObj)
			{
				GUI.backgroundColor = Color.red;
			}
			else
			{
				GUI.backgroundColor = Color.green;
			}
			if (GUILayout.Button(transform.name, new GUILayoutOption[0]))
			{
				if (transform.gameObject != this.selectedObj)
				{
					this.selectedObj = transform.gameObject;
					this.memberObj = transform.gameObject;
					this.editField = null;
					this.editProp = null;
				}
				else
				{
					this.selectedObj = this.rootObj;
					this.memberObj = this.rootObj;
					this.editField = null;
					this.editProp = null;
				}
			}
			this.AddChild(transform.gameObject);
		}
	}

	// Token: 0x06000B61 RID: 2913 RVA: 0x00045B64 File Offset: 0x00043D64
	private void comWindowFunction(int windowID)
	{
		if (this.selectedObj != null)
		{
			this.comScrollPosition = GUILayout.BeginScrollView(this.comScrollPosition, new GUILayoutOption[]
			{
				GUILayout.Width(195f),
				GUILayout.Height(500f)
			});
			foreach (Component component in this.selectedObj.GetComponents(typeof(Component)))
			{
				if (component != this.memberObj)
				{
					GUI.backgroundColor = Color.red;
				}
				else
				{
					GUI.backgroundColor = Color.green;
				}
				if (GUILayout.Button(component.GetType().ToString(), new GUILayoutOption[]
				{
					GUILayout.Height(30f)
				}))
				{
					if (component != this.memberObj)
					{
						this.memberObj = component;
						this.editField = null;
						this.editProp = null;
					}
					else
					{
						this.memberObj = this.selectedObj;
						this.editField = null;
						this.editProp = null;
					}
				}
			}
			GUILayout.EndScrollView();
		}
		GUI.DragWindow();
	}

	// Token: 0x06000B62 RID: 2914 RVA: 0x00045C74 File Offset: 0x00043E74
	private void editWindowFunction(int windowID)
	{
		this.editScrollPosition = GUILayout.BeginScrollView(this.editScrollPosition, new GUILayoutOption[]
		{
			GUILayout.Width(195f),
			GUILayout.Height(500f)
		});
		object obj = null;
		if (this.editField != null)
		{
			obj = this.editField.GetValue(this.memberObj);
		}
		if (this.editProp != null)
		{
			obj = this.editProp.GetValue(this.memberObj, null);
		}
		if (obj == null)
		{
			GUILayout.Label("Unsupported Type", new GUILayoutOption[0]);
		}
		else
		{
			if (GUILayout.Button("Go to " + obj.GetType().ToString(), new GUILayoutOption[]
			{
				GUILayout.Height(30f)
			}))
			{
				this.memberObj = (Object)obj;
				this.editField = null;
				this.editProp = null;
			}
			GUILayout.Label(obj.GetType().ToString(), new GUILayoutOption[0]);
			GUILayout.Label("Current : " + obj.ToString(), new GUILayoutOption[0]);
			if (obj.GetType().ToString() == "System.Boolean")
			{
				GUI.backgroundColor = Color.blue;
				if (GUILayout.Button("Toggle", new GUILayoutOption[0]))
				{
					bool flag = !(bool)obj;
					if (this.editProp != null)
					{
						this.editProp.SetValue(this.memberObj, flag, null);
					}
					if (this.editField != null)
					{
						this.editField.SetValue(this.memberObj, flag);
					}
				}
			}
			if (obj.GetType().ToString() == "System.Single")
			{
				GUI.backgroundColor = Color.blue;
				if (this.bGetValue)
				{
					this.objVal1 = ((float)obj).ToString();
				}
				this.objVal1 = GUILayout.TextField(this.objVal1, new GUILayoutOption[0]);
				if (GUILayout.Button("Update Value", new GUILayoutOption[0]))
				{
					if (this.editProp != null)
					{
						this.editProp.SetValue(this.memberObj, float.Parse(this.objVal1), null);
					}
					if (this.editField != null)
					{
						this.editField.SetValue(this.memberObj, float.Parse(this.objVal1));
					}
				}
			}
			if (obj.GetType().ToString() == "System.Int32")
			{
				GUI.backgroundColor = Color.blue;
				if (this.bGetValue)
				{
					this.objVal1 = ((int)obj).ToString();
				}
				this.objVal1 = GUILayout.TextField(this.objVal1, new GUILayoutOption[0]);
				if (GUILayout.Button("Update Value", new GUILayoutOption[0]))
				{
					if (this.editProp != null)
					{
						this.editProp.SetValue(this.memberObj, int.Parse(this.objVal1), null);
					}
					if (this.editField != null)
					{
						this.editField.SetValue(this.memberObj, int.Parse(this.objVal1));
					}
				}
			}
			if (obj.GetType().ToString() == "System.String")
			{
				GUI.backgroundColor = Color.blue;
				if (this.bGetValue)
				{
					this.objVal1 = ((string)obj).ToString();
				}
				this.objVal1 = GUILayout.TextField(this.objVal1, new GUILayoutOption[0]);
				if (GUILayout.Button("Update Value", new GUILayoutOption[0]))
				{
					if (this.editProp != null)
					{
						this.editProp.SetValue(this.memberObj, this.objVal1, null);
					}
					if (this.editField != null)
					{
						this.editField.SetValue(this.memberObj, this.objVal1);
					}
				}
			}
			if (obj.GetType().ToString() == "UnityEngine.Vector3")
			{
				GUI.backgroundColor = Color.blue;
				if (this.bGetValue)
				{
					Vector3 vector = (Vector3)obj;
					this.objVal1 = vector.x.ToString();
					this.objVal2 = vector.y.ToString();
					this.objVal3 = vector.z.ToString();
				}
				GUILayout.BeginHorizontal(new GUILayoutOption[0]);
				this.objVal1 = GUILayout.TextField(this.objVal1, new GUILayoutOption[0]);
				this.objVal2 = GUILayout.TextField(this.objVal2, new GUILayoutOption[0]);
				this.objVal3 = GUILayout.TextField(this.objVal3, new GUILayoutOption[0]);
				GUILayout.EndHorizontal();
				if (GUILayout.Button("Update Value", new GUILayoutOption[0]))
				{
					if (this.editProp != null)
					{
						this.editProp.SetValue(this.memberObj, new Vector3(float.Parse(this.objVal1), float.Parse(this.objVal2), float.Parse(this.objVal3)), null);
					}
					if (this.editField != null)
					{
						this.editField.SetValue(this.memberObj, new Vector3(float.Parse(this.objVal1), float.Parse(this.objVal2), float.Parse(this.objVal3)));
					}
				}
			}
			if (obj.GetType().ToString() == "UnityEngine.Vector2")
			{
				GUI.backgroundColor = Color.blue;
				if (this.bGetValue)
				{
					Vector2 vector2 = (Vector2)obj;
					this.objVal1 = vector2.x.ToString();
					this.objVal2 = vector2.y.ToString();
				}
				GUILayout.BeginHorizontal(new GUILayoutOption[0]);
				this.objVal1 = GUILayout.TextField(this.objVal1, new GUILayoutOption[0]);
				this.objVal2 = GUILayout.TextField(this.objVal2, new GUILayoutOption[0]);
				GUILayout.EndHorizontal();
				if (GUILayout.Button("Update Value", new GUILayoutOption[0]))
				{
					if (this.editProp != null)
					{
						this.editProp.SetValue(this.memberObj, new Vector2(float.Parse(this.objVal1), float.Parse(this.objVal2)), null);
					}
					if (this.editField != null)
					{
						this.editField.SetValue(this.memberObj, new Vector2(float.Parse(this.objVal1), float.Parse(this.objVal2)));
					}
				}
			}
			if (obj.GetType().ToString() == "UnityEngine.Rect")
			{
				GUI.backgroundColor = Color.blue;
				if (this.bGetValue)
				{
					Rect rect = (Rect)obj;
					this.objVal1 = rect.x.ToString();
					this.objVal2 = rect.y.ToString();
					this.objVal3 = rect.width.ToString();
					this.objVal4 = rect.height.ToString();
				}
				GUILayout.BeginHorizontal(new GUILayoutOption[0]);
				this.objVal1 = GUILayout.TextField(this.objVal1, new GUILayoutOption[0]);
				this.objVal2 = GUILayout.TextField(this.objVal2, new GUILayoutOption[0]);
				this.objVal3 = GUILayout.TextField(this.objVal3, new GUILayoutOption[0]);
				this.objVal4 = GUILayout.TextField(this.objVal4, new GUILayoutOption[0]);
				GUILayout.EndHorizontal();
				if (GUILayout.Button("Update Value", new GUILayoutOption[0]))
				{
					if (this.editProp != null)
					{
						this.editProp.SetValue(this.memberObj, new Rect(float.Parse(this.objVal1), float.Parse(this.objVal2), float.Parse(this.objVal3), float.Parse(this.objVal4)), null);
					}
					if (this.editField != null)
					{
						this.editField.SetValue(this.memberObj, new Rect(float.Parse(this.objVal1), float.Parse(this.objVal2), float.Parse(this.objVal3), float.Parse(this.objVal4)));
					}
				}
			}
			if (obj.GetType().ToString() == "UnityEngine.Color")
			{
				GUI.backgroundColor = Color.blue;
				if (this.bGetValue)
				{
					Color color = (Color)obj;
					this.objVal1 = color.r.ToString();
					this.objVal2 = color.g.ToString();
					this.objVal3 = color.b.ToString();
					this.objVal4 = color.a.ToString();
				}
				GUILayout.BeginHorizontal(new GUILayoutOption[0]);
				this.objVal1 = GUILayout.TextField(this.objVal1, new GUILayoutOption[0]);
				this.objVal2 = GUILayout.TextField(this.objVal2, new GUILayoutOption[0]);
				this.objVal3 = GUILayout.TextField(this.objVal3, new GUILayoutOption[0]);
				this.objVal4 = GUILayout.TextField(this.objVal4, new GUILayoutOption[0]);
				GUILayout.EndHorizontal();
				if (GUILayout.Button("Update Value", new GUILayoutOption[0]))
				{
					if (this.editProp != null)
					{
						this.editProp.SetValue(this.memberObj, new Color(float.Parse(this.objVal1), float.Parse(this.objVal2), float.Parse(this.objVal3), float.Parse(this.objVal4)), null);
					}
					if (this.editField != null)
					{
						this.editField.SetValue(this.memberObj, new Color(float.Parse(this.objVal1), float.Parse(this.objVal2), float.Parse(this.objVal3), float.Parse(this.objVal4)));
					}
				}
			}
			if (obj.GetType().IsEnum)
			{
				foreach (object obj2 in Enum.GetValues(obj.GetType()))
				{
					if (GUILayout.Button(obj2.ToString(), new GUILayoutOption[]
					{
						GUILayout.Height(30f)
					}))
					{
						if (this.editProp != null)
						{
							this.editProp.SetValue(this.memberObj, Enum.Parse(obj.GetType(), obj2.ToString()), null);
						}
						if (this.editField != null)
						{
							this.editField.SetValue(this.memberObj, Enum.Parse(obj.GetType(), obj2.ToString()));
						}
					}
				}
			}
		}
		if (ObjEdit.gameobjectTools && GUILayout.Button("Object Settings: <color=lime>ON</color>", new GUILayoutOption[]
		{
			GUILayout.Width(170f),
			GUILayout.Height(30f)
		}))
		{
			ObjEdit.gameobjectTools = false;
		}
		if (!ObjEdit.gameobjectTools && GUILayout.Button("Object Settings: <color=red>OFF</color>", new GUILayoutOption[]
		{
			GUILayout.Width(170f),
			GUILayout.Height(30f)
		}))
		{
			ObjEdit.gameobjectTools = true;
		}
		GUI.backgroundColor = Color.yellow;
		if (ObjEdit.gameobjectTools)
		{
			if (this.memberObj != null && GUILayout.Button("Delete Object", new GUILayoutOption[]
			{
				GUILayout.Width(175f),
				GUILayout.Height(35f)
			}))
			{
				Object.Destroy(this.memberObj);
			}
			if (this.selectedObj != null)
			{
				if (GUILayout.Button("Clone Object", new GUILayoutOption[]
				{
					GUILayout.Width(175f),
					GUILayout.Height(35f)
				}))
				{
					Object.Instantiate<GameObject>(this.selectedObj).transform.localPosition = ModMenu2.player.transform.position + new Vector3(0f, 0.5f, -2f);
				}
				if (GUILayout.Button("<b><color=navy>Components</color></b>", new GUILayoutOption[]
				{
					GUILayout.Width(170f),
					GUILayout.Height(30f)
				}))
				{
					this.comp = !this.comp;
				}
				if (this.comp)
				{
					GUILayout.Label("<i><color=navy>Get Components:</color></i> ", new GUILayoutOption[0]);
					if (GUILayout.Button("UnityEngine.Rigidbody", new GUILayoutOption[]
					{
						GUILayout.Height(30f)
					}))
					{
						this.selectedObj.gameObject.AddComponent<Rigidbody>();
					}
					if (GUILayout.Button("UnityEngine.CharacterController", new GUILayoutOption[]
					{
						GUILayout.Height(30f)
					}))
					{
						this.selectedObj.gameObject.AddComponent<CharacterController>();
					}
					if (GUILayout.Button("UnityEngine.SphereCollider", new GUILayoutOption[]
					{
						GUILayout.Height(30f)
					}))
					{
						this.selectedObj.gameObject.AddComponent<SphereCollider>();
					}
					if (GUILayout.Button("UnityEngine.CapsuleCollider", new GUILayoutOption[]
					{
						GUILayout.Height(30f)
					}))
					{
						this.selectedObj.gameObject.AddComponent<CapsuleCollider>();
					}
					if (GUILayout.Button("UnityEngine.BoxCollider", new GUILayoutOption[]
					{
						GUILayout.Height(30f)
					}))
					{
						this.selectedObj.gameObject.AddComponent<BoxCollider>();
					}
					if (GUILayout.Button("UnityEngine.WheelCollider", new GUILayoutOption[]
					{
						GUILayout.Height(30f)
					}))
					{
						this.selectedObj.gameObject.AddComponent<WheelCollider>();
					}
					if (ObjEdit.customComp && GUILayout.Button("Custom menu: <color=lime>ON</color>", new GUILayoutOption[]
					{
						GUILayout.Height(30f)
					}))
					{
						ObjEdit.customComp = false;
					}
					if (!ObjEdit.customComp && GUILayout.Button("Custom menu: <color=red>OFF</color>", new GUILayoutOption[]
					{
						GUILayout.Height(30f)
					}))
					{
						ObjEdit.customComp = true;
					}
					if (ObjEdit.customComp)
					{
						GUILayout.Label("<i><color=magenta>Custom:</color></i>", new GUILayoutOption[0]);
						this.addComTextField = GUILayout.TextArea(this.addComTextField, new GUILayoutOption[]
						{
							GUILayout.Width(175f),
							GUILayout.Height(35f)
						});
						if (GUILayout.Button("Add Component", new GUILayoutOption[]
						{
							GUILayout.Width(175f),
							GUILayout.Height(35f)
						}))
						{
							this.selectedObj.gameObject.AddComponent(Type.GetType(this.addComTextField));
						}
					}
				}
			}
			GUILayout.Label("<i><color=maroon>Object Settings:</color></i>", new GUILayoutOption[0]);
			if (GUILayout.Button("<b><color=orange>Velocity</color></b>", new GUILayoutOption[]
			{
				GUILayout.Width(170f),
				GUILayout.Height(30f)
			}))
			{
				this.vel = !this.vel;
			}
			if (this.vel)
			{
				GUILayout.Label("<color=orange>Velocity:</color>", new GUILayoutOption[0]);
				GUILayout.Label("<color=red>X:</color> " + this.velX, new GUILayoutOption[0]);
				this.velX = GUILayout.HorizontalSlider(this.velX, -10f, 10f, new GUILayoutOption[0]);
				GUILayout.Label("<color=lime>Y:</color> " + this.velY, new GUILayoutOption[0]);
				this.velY = GUILayout.HorizontalSlider(this.velY, -10f, 10f, new GUILayoutOption[0]);
				GUILayout.Label("<color=blue>Z:</color> " + this.velZ, new GUILayoutOption[0]);
				this.velZ = GUILayout.HorizontalSlider(this.velZ, -10f, 10f, new GUILayoutOption[0]);
				if (GUILayout.Button("Default", new GUILayoutOption[0]))
				{
					this.velX = 0f;
					this.velY = 0f;
					this.velZ = 0f;
				}
				if (GUILayout.Button("Get Velocity", new GUILayoutOption[0]))
				{
					this.selectedObj.GetComponent<Rigidbody>().velocity = new Vector3(this.velX, this.velY, this.velZ);
				}
			}
			if (GUILayout.Button("<b><color=aqua>Rotation</color></b>", new GUILayoutOption[]
			{
				GUILayout.Width(170f),
				GUILayout.Height(30f)
			}))
			{
				this.rot = !this.rot;
			}
			if (this.rot)
			{
				GUILayout.Label("<color=aqua>Rotate:</color>", new GUILayoutOption[0]);
				GUILayout.Label("<color=red>X:</color>", new GUILayoutOption[0]);
				if (GUILayout.Button("+", new GUILayoutOption[0]))
				{
					this.selectedObj.transform.Rotate(15f, 0f, 0f);
				}
				if (GUILayout.Button("-", new GUILayoutOption[0]))
				{
					this.selectedObj.transform.Rotate(-15f, 0f, 0f);
				}
				GUILayout.Label("<color=lime>Y:</color>", new GUILayoutOption[0]);
				if (GUILayout.Button("+", new GUILayoutOption[0]))
				{
					this.selectedObj.transform.Rotate(0f, 15f, 0f);
				}
				if (GUILayout.Button("-", new GUILayoutOption[0]))
				{
					this.selectedObj.transform.Rotate(0f, -15f, 0f);
				}
				GUILayout.Label("<color=blue>Z:</color>", new GUILayoutOption[0]);
				if (GUILayout.Button("+", new GUILayoutOption[0]))
				{
					this.selectedObj.transform.Rotate(0f, 0f, 15f);
				}
				if (GUILayout.Button("-", new GUILayoutOption[0]))
				{
					this.selectedObj.transform.Rotate(0f, 0f, -15f);
				}
			}
			if (GUILayout.Button("<b><color=green>Teleport</color></b>", new GUILayoutOption[]
			{
				GUILayout.Width(170f),
				GUILayout.Height(30f)
			}))
			{
				this.tp = !this.tp;
			}
			if (this.tp)
			{
				if (GUILayout.Button("Tp '" + this.selectedObj.name + "' to Player", new GUILayoutOption[]
				{
					GUILayout.Width(175f),
					GUILayout.Height(35f)
				}))
				{
					this.selectedObj.transform.position = ModMenu2.player.transform.position;
				}
				if (GUILayout.Button("Tp Player to '" + this.selectedObj.name + "'", new GUILayoutOption[]
				{
					GUILayout.Width(175f),
					GUILayout.Height(35f)
				}))
				{
					ModMenu2.player.transform.position = this.selectedObj.transform.position;
				}
			}
		}
		GUILayout.EndScrollView();
		GUI.DragWindow();
		this.bGetValue = false;
	}

	// Token: 0x06000B63 RID: 2915 RVA: 0x00046EAC File Offset: 0x000450AC
	private void memWindowFunction(int windowID)
	{
		GUI.backgroundColor = Color.black;
		this.memFilter = GUILayout.TextField(this.memFilter, new GUILayoutOption[0]);
		if (this.memberObj != null)
		{
			this.memScrollPosition = GUILayout.BeginScrollView(this.memScrollPosition, new GUILayoutOption[]
			{
				GUILayout.Width(195f),
				GUILayout.Height(500f)
			});
			foreach (FieldInfo fieldInfo in this.memberObj.GetType().GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
			{
				if (!(this.memFilter != "") || fieldInfo.Name.IndexOf(this.memFilter, StringComparison.OrdinalIgnoreCase) >= 0)
				{
					if (fieldInfo != this.editField)
					{
						GUI.backgroundColor = Color.red;
					}
					else
					{
						GUI.backgroundColor = Color.green;
					}
					GUILayout.BeginHorizontal(new GUILayoutOption[0]);
					if (GUILayout.Button(fieldInfo.Name, new GUILayoutOption[0]))
					{
						this.editField = fieldInfo;
						this.editProp = null;
						this.bGetValue = true;
					}
					GUILayout.EndHorizontal();
				}
			}
			foreach (PropertyInfo propertyInfo in this.memberObj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
			{
				if (!(this.memFilter != "") || propertyInfo.Name.IndexOf(this.memFilter, StringComparison.OrdinalIgnoreCase) >= 0)
				{
					if (propertyInfo != this.editProp)
					{
						GUI.backgroundColor = Color.red;
					}
					else
					{
						GUI.backgroundColor = Color.green;
					}
					if (propertyInfo.GetIndexParameters().Length == 0)
					{
						GUILayout.BeginHorizontal(new GUILayoutOption[0]);
						if (GUILayout.Button(propertyInfo.Name, new GUILayoutOption[0]))
						{
							this.editField = null;
							this.editProp = propertyInfo;
							this.bGetValue = true;
						}
						GUILayout.EndHorizontal();
					}
					else if (GUILayout.Button(propertyInfo.Name + " <INDEXED>", new GUILayoutOption[0]))
					{
						this.editField = null;
						this.editProp = null;
					}
				}
			}
			GUILayout.EndScrollView();
		}
		GUI.DragWindow();
	}

	// Token: 0x06000B64 RID: 2916 RVA: 0x000470B8 File Offset: 0x000452B8
	private void objWindowFunction(int windowID)
	{
		this.nextUpdate += Time.deltaTime;
		if (this.nextUpdate > 1f)
		{
			this.nextUpdate = 0f;
			this.objs = (Object.FindObjectsOfType(typeof(GameObject)) as GameObject[]);
		}
		this.objFilter = GUILayout.TextField(this.objFilter, new GUILayoutOption[0]);
		this.objScrollPosition = GUILayout.BeginScrollView(this.objScrollPosition, new GUILayoutOption[]
		{
			GUILayout.Width(195f),
			GUILayout.Height(500f)
		});
		if (this.objs != null)
		{
			foreach (GameObject gameObject in this.objs)
			{
				if (gameObject.transform.parent == null)
				{
					if (gameObject != this.rootObj)
					{
						GUI.backgroundColor = Color.red;
					}
					else
					{
						GUI.backgroundColor = Color.green;
					}
					if (!(this.objFilter != "") || gameObject.name.IndexOf(this.objFilter, StringComparison.OrdinalIgnoreCase) >= 0)
					{
						if (GUILayout.Button(gameObject.name, new GUILayoutOption[0]))
						{
							if (this.rootObj != gameObject)
							{
								this.rootObj = gameObject;
								this.selectedObj = gameObject;
								this.memberObj = gameObject;
								this.editField = null;
								this.editProp = null;
							}
							else
							{
								this.rootObj = null;
								this.selectedObj = null;
								this.memberObj = null;
								this.editField = null;
								this.editProp = null;
							}
						}
						if (gameObject == this.rootObj)
						{
							this.AddChild(gameObject);
						}
						if (gameObject.name == "New Game Object")
						{
							Object.Destroy(gameObject);
						}
					}
				}
			}
		}
		GUILayout.EndScrollView();
		GUI.DragWindow();
	}

	// Token: 0x06000B65 RID: 2917 RVA: 0x00047280 File Offset: 0x00045480
	private void OnGUI()
	{
		if (GUI.Button(new Rect(595f, 0f, 150f, 50f), "<b><color=lime><color=red>$</color>Editor 2.3<color=blue>$</color></color></b>"))
		{
			this.bActive = !this.bActive;
		}
		if (this.bActive)
		{
			GUI.backgroundColor = Color.black;
			this.objWindowRect = GUILayout.Window(4, this.objWindowRect, new GUI.WindowFunction(this.objWindowFunction), "Objects", new GUILayoutOption[0]);
			if (this.selectedObj != null)
			{
				this.comWindowRect = GUILayout.Window(5, this.comWindowRect, new GUI.WindowFunction(this.comWindowFunction), "Components", new GUILayoutOption[0]);
			}
			if (this.selectedObj != null)
			{
				this.memWindowRect = GUILayout.Window(6, this.memWindowRect, new GUI.WindowFunction(this.memWindowFunction), (this.memberObj == null) ? "Members" : this.memberObj.name, new GUILayoutOption[0]);
			}
			if (this.editField != null || this.editProp != null)
			{
				this.editWindowRect = GUILayout.Window(7, this.editWindowRect, new GUI.WindowFunction(this.editWindowFunction), "Edit", new GUILayoutOption[0]);
				return;
			}
		}
		else
		{
			this.rootObj = null;
			this.selectedObj = null;
			this.memberObj = null;
			this.editField = null;
			this.editProp = null;
			this.objs = null;
		}
	}

	// Token: 0x06000B66 RID: 2918 RVA: 0x0000762E File Offset: 0x0000582E
	public ObjEdit()
	{
	}

	// Token: 0x06000B67 RID: 2919 RVA: 0x000473EC File Offset: 0x000455EC
	private void Start()
	{
		this.objVal1 = string.Empty;
		this.objVal2 = string.Empty;
		this.objVal3 = string.Empty;
		this.objVal4 = string.Empty;
		this.objWindowRect = new Rect(40f, 70f, 170f, 50f);
		this.comWindowRect = new Rect(270f, 70f, 170f, 50f);
		this.memWindowRect = new Rect(480f, 70f, 170f, 50f);
		this.editWindowRect = new Rect(750f, 70f, 170f, 50f);
	}

	// Token: 0x04000F7B RID: 3963
	public bool bActive;

	// Token: 0x04000F7C RID: 3964
	private GameObject[] objs;

	// Token: 0x04000F7D RID: 3965
	public string objVal1;

	// Token: 0x04000F7E RID: 3966
	public string objVal2;

	// Token: 0x04000F7F RID: 3967
	public string objVal3;

	// Token: 0x04000F80 RID: 3968
	public string objVal4;

	// Token: 0x04000F81 RID: 3969
	public bool bGetValue;

	// Token: 0x04000F82 RID: 3970
	public string objFilter = string.Empty;

	// Token: 0x04000F83 RID: 3971
	public string memFilter = string.Empty;

	// Token: 0x04000F84 RID: 3972
	public Rect objWindowRect;

	// Token: 0x04000F85 RID: 3973
	public Vector2 objScrollPosition;

	// Token: 0x04000F86 RID: 3974
	public Rect comWindowRect;

	// Token: 0x04000F87 RID: 3975
	public Vector2 comScrollPosition;

	// Token: 0x04000F88 RID: 3976
	public Rect memWindowRect;

	// Token: 0x04000F89 RID: 3977
	public Vector2 memScrollPosition;

	// Token: 0x04000F8A RID: 3978
	public Rect editWindowRect;

	// Token: 0x04000F8B RID: 3979
	public Vector2 editScrollPosition;

	// Token: 0x04000F8C RID: 3980
	public GameObject rootObj;

	// Token: 0x04000F8D RID: 3981
	public GameObject selectedObj;

	// Token: 0x04000F8E RID: 3982
	public Object memberObj;

	// Token: 0x04000F8F RID: 3983
	public FieldInfo editField;

	// Token: 0x04000F90 RID: 3984
	public PropertyInfo editProp;

	// Token: 0x04000F91 RID: 3985
	public float nextUpdate;

	// Token: 0x04000F92 RID: 3986
	public string objVal5;

	// Token: 0x04000F93 RID: 3987
	public string objVal6;

	// Token: 0x04000F94 RID: 3988
	public string addComTextField;

	// Token: 0x04000F95 RID: 3989
	public static bool gameobjectTools;

	// Token: 0x04000F96 RID: 3990
	public static bool customComp;

	// Token: 0x04000F97 RID: 3991
	public float velX;

	// Token: 0x04000F98 RID: 3992
	public float velY;

	// Token: 0x04000F99 RID: 3993
	public float velZ;

	// Token: 0x04000F9A RID: 3994
	public bool vel;

	// Token: 0x04000F9B RID: 3995
	public bool rot;

	// Token: 0x04000F9C RID: 3996
	public bool tp;

	// Token: 0x04000F9D RID: 3997
	public bool comp;
}
