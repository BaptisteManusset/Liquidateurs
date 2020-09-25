using UnityEngine;
using TMPro;

public class Stock : Interactable
{
    [SerializeField] TextMeshPro scoreText;
    [SerializeField] Transform plane;
    public int score;
    public int maxBenne = 100;


    public override void Interact(Liquidateur li)
    {
        score += li.stock;
        li.stock = 0;
        scoreText.text = score.ToString();

        UpdatePlane();
    }

    //bouge selon le score la quantité de dechet dans la benne
    private void UpdatePlane()
    {
        Vector3 pos = plane.position;
        pos.y = Tools.Remap(score % maxBenne, 0, maxBenne, 0, 1.5f);

        plane.position = pos;
    }
}
