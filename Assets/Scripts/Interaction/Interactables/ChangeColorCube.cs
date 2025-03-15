using UnityEngine;

public class ChangeColorCube : Interactable
{
    private MeshRenderer mesh;
    
    [SerializeField] private Color[] colors;

    private void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        mesh.material.color = Color.red;
    }

    protected override void Interact()
    {
        var colorIndex = Random.Range(0, colors.Length - 1);
        mesh.material.color = colors[colorIndex];
    }
}
