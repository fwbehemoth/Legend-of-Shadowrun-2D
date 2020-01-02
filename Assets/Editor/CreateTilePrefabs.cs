using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CreateTilePrefabs : EditorWindow {
    string fileName = "";
    string filePath = "";

    [MenuItem("Tools/Create Floor Tile Prefabs")]
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
        TextureImporter textureImporter = TextureImporter.GetAtPath("Assets/" + filePath + fileName) as TextureImporter;
        Texture2D image = AssetDatabase.LoadAssetAtPath("Assets/" + filePath + fileName, typeof(Texture2D)) as Texture2D;
        SpriteMetaData[] spriteMetaDataArray = textureImporter.spritesheet;
        int count = 0;
        foreach (SpriteMetaData spriteMetaData in spriteMetaDataArray){
            GameObject spritePrefab = new GameObject();
            spritePrefab.AddComponent<SpriteRenderer>();
            spritePrefab.name = image.name + "_" + count;
            spritePrefab.layer = 8;
            spritePrefab.tag = "Ground";
            Sprite sprite = Sprite.Create(image, spriteMetaData.rect, spriteMetaData.pivot);
            sprite.name = image.name + "_" + count;
            SpriteRenderer spriteRenderer = spritePrefab.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = sprite;
            spriteRenderer.sortingLayerName = "Ground";
            if(!AssetDatabase.IsValidFolder("Assets/Sprites/Tiles/" + image.name)) {
                AssetDatabase.CreateFolder("Assets/Sprites/Tiles", image.name);
            }
            AssetDatabase.CreateAsset(sprite, "Assets/Sprites/Tiles/" + image.name + "/" + sprite.name + ".asset");
//            if(!AssetDatabase.IsValidFolder("Assets/Prefabs/Tiles/" + image.name)) {
//                AssetDatabase.CreateFolder("Assets/Prefabs/Tiles", image.name);
//            }
//            PrefabUtility.SaveAsPrefabAsset(spritePrefab, "Assets/Prefabs/Tiles/" + image.name + "/" + spritePrefab.name + ".prefab" );

            count++;
        }
    }
}
