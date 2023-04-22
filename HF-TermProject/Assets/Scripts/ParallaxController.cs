using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    public HorizontalDirection horizontalDirection;
    public FloatReference speedMultiplier;
    public MoveType moveType;
    public Transform transformToFollow;
    public VerticalDirection verticalDirection;

    private List<ParallaxImage> imgs;
    private float lastX;
    private float lastY;

    private void Start()
    {
        InitController();
    }
    private void FixedUpdate()
    {
        if (imgs == null) return;

        if (moveType == MoveType.Overtime) MoveOverTime();
        else if (moveType == MoveType.FollowTransform)
        {
            FollowTransformX();
            FollowTransformY();
        }
    }

    private void MoveOverTime()
    {
        if (horizontalDirection == HorizontalDirection.Fix) return;
            foreach (var item in imgs)
            {
                item.MoveX(Time.deltaTime);
            }
    }

    private void FollowTransformX()
    {
        if (horizontalDirection == HorizontalDirection.Fix) return;

        float distance = lastX - transformToFollow.position.x;
        if (Mathf.Abs(distance) < 0.001f) return;
        foreach (var item in imgs)
        {
            item.MoveX(distance);
        }
        lastX = transformToFollow.position.x;
    }
    private void FollowTransformY()
    {
        if (verticalDirection == VerticalDirection.Fix) return;

        float distance = lastY - transformToFollow.position.y;
        if (Mathf.Abs(distance) < 0.001f) return;
        foreach (var item in imgs)
        {
            item.MoveY(distance);
        }
        lastY = transformToFollow.position.y;
    }

    private void InitController()
    {
        InitList();

        ScanForImages();

        foreach(var item in imgs)
        {
            item.InitImage(speedMultiplier, horizontalDirection, verticalDirection, moveType == MoveType.FollowTransform);
        }
        if (moveType == MoveType.FollowTransform)
        {
            lastX = transformToFollow.position.x;
            lastY = transformToFollow.position.y;
        }
    }

    private void InitList()
    {
        if (imgs == null) imgs = new List<ParallaxImage>();
        else
        {
            foreach (var item in imgs)
            {
                item.CleanUp();
            }
            imgs.Clear();
        }
    }

    private void ScanForImages()
    {
        ParallaxImage pi;
        foreach(Transform child in transform)
        {
            if (child.gameObject.activeSelf)
            {
                pi = child.GetComponent<ParallaxImage>();
                if (pi != null) imgs.Add(pi);
            }
        }
    }
}
[System.Serializable]
public class FloatReference
{
    [Range(0.01f,5f)]
    public float value = 1;
}

public enum HorizontalDirection
{
    Fix,
    Left,
    Right
}

public enum VerticalDirection
{
    Fix,
    Up,
    Down
}
public enum MoveType
{
    Overtime,
    FollowTransform
}