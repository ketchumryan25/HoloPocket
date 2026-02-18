using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.U2D;
using UnityEngine.U2D.Animation; // Namespace for SpriteLibrary

[UnitTitle("Get Sprite from Library")]
[UnitCategory("Sprites")]
public class GetSpriteFromLibraryNode : Unit
{
    // Input: GameObject with SpriteLibrary component
    [DoNotSerialize]
    public ValueInput gameObjectInput;

    // Input: Category label
    [DoNotSerialize]
    public ValueInput categoryLabel;

    // Input: Sprite label within category
    [DoNotSerialize]
    public ValueInput spriteLabel;

    // Output: Retrieved sprite
    [DoNotSerialize]
    public ValueOutput spriteOutput;

    protected override void Definition()
    {
        gameObjectInput = ValueInput<GameObject>("GameObject");
        categoryLabel = ValueInput<string>("Category");
        spriteLabel = ValueInput<string>("Sprite");

        spriteOutput = ValueOutput<Sprite>("Sprite", GetSprite);

        Requirement(gameObjectInput, spriteOutput);
        Requirement(categoryLabel, spriteOutput);
        Requirement(spriteLabel, spriteOutput);
    }

    private Sprite GetSprite(Flow flow)
    {
        GameObject go = flow.GetValue<GameObject>(gameObjectInput);
        string category = flow.GetValue<string>(categoryLabel);
        string spriteLbl = flow.GetValue<string>(spriteLabel);

        if (go == null)
        {
            Debug.LogWarning("GameObject is null");
            return null;
        }

        var spriteLibrary = go.GetComponent<SpriteLibrary>();
        if (spriteLibrary == null)
        {
            Debug.LogWarning("No SpriteLibrary component found on the GameObject");
            return null;
        }

        // Retrieve sprite using category and label
        Sprite sprite = spriteLibrary.GetSprite(category, spriteLbl);
        if (sprite == null)
        {
            Debug.LogWarning($"Sprite not found for Category: {category}, Label: {spriteLbl}");
        }
        return sprite;
    }
}