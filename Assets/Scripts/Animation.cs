using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Animation : MonoBehaviour
{
    //change the sprite that is being displayed
    public SpriteRenderer spriteRenderer { get; private set; }
    public Sprite[] sprites;
    public float waitTime = 0.25f;
    public int currentSpriteIndex { get; private set; }
    public bool loop = true;

    private void Awake()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();

    }

    private void Start()
    {
        InvokeRepeating(nameof(NextFrame), this.waitTime, this.waitTime);
    }

    private void changeSprite()
    {
        if (this.sprites.Length >= 0
        && this.currentSpriteIndex < this.sprites.Length)
        {
            //print("Changing sprite to " + this.currentSpriteIndex);
            this.spriteRenderer.sprite = this.sprites[this.currentSpriteIndex];
        }

    }

    private void NextFrame()
    {
        //if we have no sprites, return
        if (!this.spriteRenderer.enabled)
        {
            return;
        }

        this.currentSpriteIndex++;

        //if we are at the end of the array
        if (this.currentSpriteIndex >= this.sprites.Length)
        {
            //if we are looping
            if (this.loop)
            {
                //reset the index
                this.currentSpriteIndex = 0;
            }
            else
            {
                //stop the animation
                CancelInvoke(nameof(NextFrame));
                return;
            }
        }

        //change the sprite
        changeSprite();

    }

    public void restart()
    {
        this.currentSpriteIndex = -1;
        NextFrame();
    }
}
