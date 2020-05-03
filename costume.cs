//script that can be attached to any character to switch their sprite renderer to whatever i choose
//essential for optimising the workload of the game, so that I didnt need to animate every single character
//each character can now use the same animator controller because of this script

using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class costume : MonoBehaviour
{
    public string SpriteSheetName;
    private string LoadedSpriteSheetName;
    private Dictionary<string, Sprite> spriteSheet;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.LoadSpriteSheet();
    }

    private void LateUpdate()
    {
        if (this.LoadedSpriteSheetName != this.SpriteSheetName)
        {
            this.LoadSpriteSheet();
        }
        spriteRenderer.sprite = spriteSheet[spriteRenderer.sprite.name];
    }

    // Loads the sprites from a sprite sheet
    private void LoadSpriteSheet()
    {
        var sprites = Resources.LoadAll<Sprite>(this.SpriteSheetName);
        this.spriteSheet = sprites.ToDictionary(x => x.name, x => x);

        this.LoadedSpriteSheetName = this.SpriteSheetName;
    }
}
