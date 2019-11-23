using UnityEditor;
using UnityEngine;

public class CreateTilePrefabs : EditorWindow {
    string fileName = "";
    string filePath = "";

    [MenuItem("Tools/Create Tile Prefabs")]
    public static void ShowWindow(){
        GetWindow<CreateTilePrefabs>();
    }

    void OnGUI(){
        GUILayout.Label("File Name", EditorStyles.boldLabel);
        fileName = EditorGUILayout.TextField("File Name", fileName);

        GUILayout.Label("File Path", EditorStyles.boldLabel);
        filePath = EditorGUILayout.TextField("File Path", filePath);

        if(GUILayout.Button("Process")){
            createTilePrefabs(fileName, filePath);
        }
    }

    void createTilePrefabs(string fileName, string filePath){
        Debug.Log("File Name: " + fileName);
        TextureImporter textureImporter = TextureImporter.GetAtPath("Assets/" + filePath + fileName) as TextureImporter;
//        TextureImporter textureImporter = AssetDatabase.LoadAssetAtPath("Assets/" + filePath + fileName, typeof(TextureImporter)) as TextureImporter;
        Debug.Log("TextureImporter: " + textureImporter);
//        Texture2D image = Resources.Load(filePath + fileName) as Texture2D;
        Texture2D image = AssetDatabase.LoadAssetAtPath("Assets/" + filePath + fileName, typeof(Texture2D)) as Texture2D;
        Debug.Log("Image: " + image);
        SpriteMetaData[] spriteMetaDataArray = textureImporter.spritesheet;
        foreach (SpriteMetaData spriteMetaData in spriteMetaDataArray){
            GameObject spritePrefab = new GameObject();
            spritePrefab.AddComponent<SpriteRenderer>();
            Sprite sprite = Sprite.Create(image, spriteMetaData.rect, spriteMetaData.pivot);
            SpriteRenderer spriteRenderer = spritePrefab.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = sprite;
            Destroy(spritePrefab);
        }
    }
}
