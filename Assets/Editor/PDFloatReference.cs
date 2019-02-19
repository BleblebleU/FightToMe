using UnityEngine;
using UnityEditor;
using System.Linq;

[CustomPropertyDrawer(typeof(FloatReference))]
public class PDFloatReference : PropertyDrawer
{
    // Draw the property inside the given rect
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Using BeginProperty / EndProperty on the parent property means that
        // prefab override logic works on the entire property.
        EditorGUI.BeginProperty(position, label, property);

        //bool useConstant = property.FindPropertyRelative("UseConstant").boolValue;

        // Draw label
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        //var rect = new Rect(position.position, Vector2.one * 20);

        //if (EditorGUI.DropdownButton(rect, new GUIContent(GetTexture()), FocusType.Keyboard, new GUIStyle() { fixedWidth = 50f, border = new RectOffset(1, 1, 1, 1) }))
        //{
        //    GenericMenu menu = new GenericMenu();
        //    menu.AddItem(new GUIContent("UseConstant"),
        //        useConstant,
        //        () => SetProperty(property, true));

        //    menu.AddItem(new GUIContent("Variable"),
        //        !useConstant,
        //        () => SetProperty(property, false));

        //    menu.ShowAsContext();
        //}

        //position.position += Vector2.right * 15;
        //float value = property.FindPropertyRelative("ConstantValue").floatValue;

        //if (useConstant == true)
        //{
        //    string newValue = EditorGUI.TextField(position, value.ToString());
        //    float.TryParse(newValue, out value);
        //    property.FindPropertyRelative("ConstantValue").floatValue = value;
        //}
        //else
        //{
        //    EditorGUI.ObjectField(position, property.FindPropertyRelative("Variable"), GUIContent.none);
        //}
        // Don't make child fields be indented
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        // Calculate rects
        var UseConstant = new Rect(position.x, position.y, 30, position.height);
        var ConstantValue = new Rect(position.x + 35, position.y, 50, position.height);
        var Variable = new Rect(position.x + 90, position.y, position.width - 90, position.height);

        // Draw fields - passs GUIContent.none to each so they are drawn without labels
        EditorGUI.PropertyField(UseConstant, property.FindPropertyRelative("UseConstant"), GUIContent.none);
        EditorGUI.PropertyField(ConstantValue, property.FindPropertyRelative("ConstantValue"), GUIContent.none);
        EditorGUI.PropertyField(Variable, property.FindPropertyRelative("Variable"), GUIContent.none);

        // Set indent back to what it was
        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }

    private void SetProperty(SerializedProperty property, bool value)
    {
        var propRelative = property.FindPropertyRelative("UseConstant");
        propRelative.boolValue = value;
        property.serializedObject.ApplyModifiedProperties();
    }

    private Texture GetTexture()
    {
        var textures = Resources.FindObjectsOfTypeAll(typeof(Texture))
            .Where(t => t.name.ToLower().Contains("animationdopesheetkeyframe"))
            .Cast<Texture>().ToList();
        return textures[0];

    }
}