using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShakyText : MonoBehaviour
{
    private TMP_Text text;
    private Mesh mesh;
    private Vector3[] vertices;

    private void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        text.ForceMeshUpdate();
        mesh = text.mesh;
        vertices = mesh.vertices;

        for (int i = 0; i < text.textInfo.characterCount; i++)
        {
            TMP_CharacterInfo charInfo = text.textInfo.characterInfo[i];

            int index = charInfo.vertexIndex;

            Vector3 offset = Shake(Time.time + i);

            vertices[index] += offset;
            vertices[index + 1] += offset;
            vertices[index + 2] += offset;
            vertices[index + 3] += offset;
        }

        mesh.vertices = vertices;
        text.canvasRenderer.SetMesh(mesh);
    }

    Vector2 Shake(float time)
    {
        return new Vector2(Mathf.Sin(time * 30.2f), Mathf.Cos(time * 10.8f));
    }
}
