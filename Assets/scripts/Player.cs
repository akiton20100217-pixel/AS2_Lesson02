using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public GameObject bulletPrefab;     // 弾丸のプレハブ
    public GameObject shotPoint;        // 弾丸の発射位置オブジェクト

    // 移動値の設定
    public float moveSpeed = 5f;       
    private Vector3 inputMoveVelocty;

    [Header("回転軸の設定")]
    public bool　tiltInvart = false; //
    public GameObject lookAxis; //向きベクトル：オブジェクト
    public GameObject gyroAzis; //ジャイロ軸　：オブジェクト
    private Vector3 lookAngles; //向きベクトル：値
    private float gyroAngles;   //ジャイロ回転：値

    //バリアの設定
    [Header("バリア設定")]
    public GameObject barrier; //バリアのオブジェクト参照
    public MeshRenderer barrierRenderer; //バリアのマテリアル参照
    public bool barrierActivation; //バリアの有効無効

    public GameObject MuzzilFlash;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    void Update()
    {
        float zSpeed = 5 * Time.deltaTime;
        transform.Translate(0, 0, zSpeed);

        // 移動方向に回転
        lookAngles.x += inputMoveVelocty.x;
        lookAngles.y += inputMoveVelocty.y * (tiltInvart ? -1 : 1);
        gyroAngles += inputMoveVelocty.x * -1;

        //徐々に0(真っすぐ)に戻す
        lookAngles = Vector3 .Lerp(lookAngles, Vector3.zero, Time.deltaTime * 3);
        gyroAngles = Mathf.Lerp(gyroAngles, 0, Time.deltaTime * 3);

        //回転の制限(制限対象値,最小値,最大値)
        lookAngles.x = Mathf.Clamp(lookAngles.x, -15f, 15f);
        lookAngles.y = Mathf.Clamp(lookAngles.y, -15f, 15f);
        gyroAngles = Mathf.Clamp(gyroAngles, -15f, 15f);

        //角度の代入 eularAnglesで角度変更ができる
        lookAxis .transform.eulerAngles = lookAngles;
    }

    // PlayerInputから[Move]アクションを呼び出すメソッド
    public void OnMove(InputValue value)
    {
        // 第一引数にPlayerInputから渡された値(InputValue)を取得する
        Debug.Log($"移動[{value.Get<Vector2>()}]");

        // 移動のベクトルを作成する
        Vector3 move = new Vector3(
            value.Get<Vector2>().x,
            value.Get<Vector2>().y,
            0);

        // X軸の移動量を制限
        if (transform.position.x + value.Get<Vector2>().x < -8
            || transform.position.x + value.Get<Vector2>().x > 8)
            return;

        // Y軸の移動量を制限
        if (transform.position.y + value.Get<Vector2>().y < -4
            || transform.position.y + value.Get<Vector2>().y > 6)
            return;

        // 値を整数に丸める
        move.x = Mathf.Round(move.x);
        move.y = Mathf.Round(move.y);

        // プレイヤーを移動させる
        transform.Translate(move);

        // 移動値を保存する
        inputMoveVelocty = move;
    }

    // PlayerInputから[Attack]アクションを呼び出すメソッド
    public void OnAttack(InputValue value)
    {
        // 第一引数にPlayerInputから渡された値(InputValue)を取得する
        Debug.Log($"攻撃アクション [{value.Get<float>()}]");

        // 弾丸を生成する
        GameObject bullet = Instantiate(bulletPrefab, shotPoint.transform.position, transform.rotation);

        // 弾丸に力を加える
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(bullet.transform.forward * 25, ForceMode.Impulse);

        // 5秒後に弾丸を破壊する
        Destroy(bullet, 5f);

        GameObject Flash = Instantiate(MuzzilFlash, shotPoint.transform.position, transform.rotation);
        Destroy(Flash, 5f);
    }

    void OnTriggerEnter(Collider collision)
    {
        // バリアが有効な場合は衝突を無視する
        if(collision.transform.tag.Equals("Item/Barrier"))
        {
            
        }
    }
}