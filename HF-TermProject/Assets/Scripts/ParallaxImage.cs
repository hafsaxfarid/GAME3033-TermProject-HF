using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxImage : MonoBehaviour
{
    public float speedX = 0;
    public float speedY = 0;
    public int spawnCount = 2;
    private const int roundFactor = 1000;

    private Transform[] controlledTransforms;
    private float imageWidth;
    private float minLeftX;
    private float maxRightX;
    private FloatReference speedMultiplier;
    private HorizontalDirection hDir;
    private VerticalDirection vDir;

    private bool followTransform;

    public void MoveX(float moveBy)
    {
        moveBy *= speedX * speedMultiplier.value;
        if (hDir == HorizontalDirection.Right) moveBy *= -1;

        moveBy = Mathf.Round(moveBy * roundFactor) / roundFactor;


        for(int i = 0; i<controlledTransforms.Length; i++)
        {
            Vector3 newPos = controlledTransforms[i].position;
            newPos.x -= moveBy;
            newPos.x = Mathf.Round(newPos.x * roundFactor) / roundFactor;
            controlledTransforms[i].position = newPos;
        }
        CheckAndReposition();
    }
    public void MoveY(float moveBy)
    {
        moveBy *= speedY * speedMultiplier.value;
        if (vDir == VerticalDirection.Down) moveBy *= -1;

        for (int i = 0; i < controlledTransforms.Length; i++)
        {
            Vector3 newPos = controlledTransforms[i].position;
            newPos.y -= moveBy;
            controlledTransforms[i].position = newPos;
        }
    }

    private void CheckAndReposition()
    {
        if(hDir == HorizontalDirection.Left || followTransform)
        {
            for (int i = 0; i < controlledTransforms.Length; i++)
            {
                if (controlledTransforms[i].position.x < minLeftX)
                {
                    Vector3 newPos = controlledTransforms[i].position;
                    newPos.x = GetRightmostTransform().position.x + imageWidth;
                    controlledTransforms[i].position = newPos;
                }
            }
        }
        else if (hDir == HorizontalDirection.Right || followTransform)
        {
            for (int i = 0; i < controlledTransforms.Length; i++)
            {
                if (controlledTransforms[i].position.x > maxRightX)
                {
                    Vector3 newPos = controlledTransforms[i].position;
                    newPos.x = GetLeftmostTransform().position.x - imageWidth;
                    controlledTransforms[i].position = newPos;
                }
            }
        }
    }

    public void CleanUp()
    {
        if(controlledTransforms != null)
        {
            for(int i = 0; i < controlledTransforms.Length; i++)
            {
                Destroy(controlledTransforms[i].gameObject);
            }
        }
    }

    public void InitImage(FloatReference speedMultiplier, HorizontalDirection hDir, VerticalDirection vDir, bool followTransform)
    {
        this.speedMultiplier = speedMultiplier;
        this.hDir = hDir;
        this.vDir = vDir;
        this.followTransform = followTransform;

        int arraySize = spawnCount;
        if (followTransform) arraySize *= 2;
        arraySize += 1;

        controlledTransforms = new Transform[arraySize];
        controlledTransforms[0] = transform;


        imageWidth = GetComponent<SpriteRenderer>().bounds.size.x;
        if (followTransform)
        {
            minLeftX = transform.position.x - imageWidth * (spawnCount + 1) - 0.5f;
            maxRightX = transform.position.x + imageWidth * (spawnCount + 1) + 0.5f;
        }
        else
        {
            if (hDir == HorizontalDirection.Left)
            {
                minLeftX = transform.position.x - imageWidth - 0.5f;
                maxRightX = float.PositiveInfinity;
            }
            else if (hDir == HorizontalDirection.Right)
            {
                maxRightX = transform.position.x + imageWidth + 0.5f;
                minLeftX = float.NegativeInfinity;
            }
            else if (hDir == HorizontalDirection.Fix)
            {
                minLeftX = float.NegativeInfinity;
                maxRightX = float.PositiveInfinity;
            }
        }
        if (hDir == HorizontalDirection.Left)
        {
            minLeftX = transform.position.x - imageWidth - 0.5f;
            maxRightX = float.PositiveInfinity;
        }
        else if (hDir == HorizontalDirection.Right)
        {
            maxRightX = transform.position.x + imageWidth + 0.5f;
            minLeftX = float.NegativeInfinity;
        }
        else if (hDir == HorizontalDirection.Fix)
        {
            minLeftX = float.NegativeInfinity;
            maxRightX = float.PositiveInfinity;
        }

        float changeBy;

        for(int i = 1; i <= spawnCount; i++)
        {
            if(hDir == HorizontalDirection.Right)
            {
                changeBy = -imageWidth * i;
            }
            else
            {
                changeBy = imageWidth * i;
            }
            controlledTransforms[i] = PrepareCopyAt(transform.position.x + changeBy);
            if (followTransform) controlledTransforms[i + spawnCount] = PrepareCopyAt(transform.position.x - changeBy);
        }
    }

    private Transform PrepareCopyAt(float posX)
    {
        GameObject go = Instantiate(gameObject, new Vector3(posX, transform.position.y, transform.position.z), Quaternion.identity, transform.parent);
        Destroy(go.GetComponent<ParallaxImage>());

        return go.transform;
    }

    private Transform GetRightmostTransform()
    {
        float currentMaxX = float.NegativeInfinity;
        Transform currentTransform = null;

        for(int i = 0; i < controlledTransforms.Length; i++)
        {
            if(currentMaxX < controlledTransforms[i].position.x)
            {
                currentMaxX = controlledTransforms[i].position.x;
                currentTransform = controlledTransforms[i];
            }
        }

        return currentTransform;
    }
    private Transform GetLeftmostTransform()
    {
        float currentMinX = float.PositiveInfinity;
        Transform currentTransform = null;

        for (int i = 0; i < controlledTransforms.Length; i++)
        {
            if (currentMinX > controlledTransforms[i].position.x)
            {
                currentMinX = controlledTransforms[i].position.x;
                currentTransform = controlledTransforms[i];
            }
        }

        return currentTransform;
    }
}
