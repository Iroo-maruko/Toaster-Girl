using UnityEngine;

public class FlyingEnemyMovement : MonoBehaviour
{
    public float hoverSpeed = 2f;
    public float hoverAmount = 0.5f;
    public float hoverCenterHeight = 3.5f;

    public float detectionRadiusXZ = 4f;
    public float detectionHeight = 4f;
    public float followSpeed = 2f;

    public string playerTag = "Player";

    public Transform player;
    private float timeOffset;
    public bool isFollowingPlayer = false;

    [Header("Movement sounds")]
    public FMODUnity.EventReference Buzz;

    private Vector3 followOffset = Vector3.zero; 

    void Start()
    {
        AudioManager.Instance.PlayInstance(Buzz, transform.position, gameObject);
        timeOffset = Random.Range(0f, 100f);
    }

    void Update()
    {
        HoverMotion();

        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag(playerTag);
            if (playerObj != null) player = playerObj.transform;
        }

        if (player != null)
        {
            CheckPlayerBelow();

            if (isFollowingPlayer)
            {
                FollowPlayer();
            }
        }
    }

    void HoverMotion()
    {
        Vector3 motion = GetBeeMotion();
        transform.position = new Vector3(
            transform.position.x + motion.x * Time.deltaTime,
            hoverCenterHeight + motion.y
            //transform.position.z + motion.z * Time.deltaTime
        );
    }

    Vector3 GetBeeMotion()
    {
        float t = Time.time + timeOffset;

        float x = Mathf.Cos(t * hoverSpeed) * hoverAmount * 0.3f;
        float y = Mathf.Sin(t * hoverSpeed * 1.5f) * hoverAmount;

        return new Vector3(x, y, 0);
    }

    void CheckPlayerBelow()
    {
        Vector3 beePos = transform.position;
        Vector3 playerPos = player.position;

        float horizontalDistance = Vector2.Distance(
            new Vector2(beePos.x, beePos.z),
            new Vector2(playerPos.x, playerPos.z)
        );

        float verticalDistance = beePos.y - playerPos.y;

        if (verticalDistance > 0 && verticalDistance <= detectionHeight &&
            horizontalDistance <= detectionRadiusXZ)
        {
            if (!isFollowingPlayer)
            {
                Debug.Log("[BeeEnemy] Player detected below!");

                followOffset = new Vector3(
                    Random.Range(-1.5f, 1.5f),
                    0,
                    Random.Range(-1.5f, 1.5f)
                );
            }
            isFollowingPlayer = true;
        }
        else
        {
            isFollowingPlayer = false;
        }
    }

    void FollowPlayer()
    {
        Vector3 targetPos = new Vector3(player.position.x, transform.position.y, player.position.z) + followOffset;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, followSpeed * Time.deltaTime);
    }
}
