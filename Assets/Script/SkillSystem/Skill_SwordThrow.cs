using UnityEngine;

public class Skill_SwordThrow : Skill_Base
{
    [SerializeField] private float throwPower = 5f;
    [SerializeField] private float swordGravity = 3.5f;

    [Header("Trajectory Prediction")]
    [SerializeField] private GameObject predictionDot;
    [SerializeField] private int numberOfDots = 20;
    [SerializeField] private float spaceBetweenDots = .05f;
    private Transform[] dots; //This will have all dots collected into one array so we can control them, change their places, enable them and so on.
    private Vector2 confirmedDirection; //This will be used when we confirm direction and create the sword object and give it the direction to fly

    protected override void Awake()
    {
        base.Awake();
        dots = GenerateDots();
    }


    public void ThrowSword()
    {
        Debug.Log("acb");
    }

    public void PredictTrajectory(Vector2 direction)
    {
        for (int i = 0; i < dots.Length; i++)
        {
            dots[i].position = GetTrajectoryPoint(direction, i * spaceBetweenDots);
        }
    }

    private Vector2 GetTrajectoryPoint(Vector2 direction, float t)
    {
        float scaledThrowPower = throwPower * 10;

        //this give us the initial velocity - the starting speed and direction of the throw
        Vector2 initialVelocity = direction * scaledThrowPower;

        //gravity pulls the sword down overtime. the longer its in the air, the more it drops
        Vector2 gravityEffect = .5f * Physics2D.gravity * swordGravity * (t * t);

        //we calculate how for the sword will travel after time 't'
        //by combining the initial throw direction with gravity pull
        Vector2 pridictedPoint = (initialVelocity * t) + gravityEffect;

        Vector2 playerPosition = transform.root.position; //root give parent of the entire game obj

        return playerPosition + pridictedPoint;
    }

    public void ConfirmedTrajectory(Vector2 direction) => confirmedDirection = direction;
    public void EnableDots(bool enable)
    {
        foreach (Transform t in dots)
        {
            t.gameObject.SetActive(enable);
        }
    }

    private Transform[] GenerateDots()
    {
        Transform[] newDots = new Transform[numberOfDots];

        for (int i = 0; i < numberOfDots; i++)
        {
            newDots[i] = Instantiate(predictionDot, transform.position, Quaternion.identity,transform).transform;
            newDots[i].gameObject.SetActive(false);
        }

        return newDots;
    }
}
