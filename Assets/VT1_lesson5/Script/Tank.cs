using Mono.Cecil.Cil;
using UnityEngine;
using UnityEngine.InputSystem;



public class Tank : MonoBehaviour
{
    //serializeField
    [Header("＝＝＝オブジェクト参照＝＝＝")]
    [SerializeField]private Transform topJoint; //上部ジョイント
    [SerializeField]private Transform cannonJoint; //回転速度

    [Header("＝＝＝弾丸設定＝＝＝")]
    [SerializeField]private GameObject bulletPrefabs; //弾丸のプレハブ
    [SerializeField]private Transform shotPoint; //発射位置

    private Vector3 topAngles = Vector3.zero; //上部ジョイントの角度
    private Vector3 cannonAngles = Vector3.zero; //砲身の角度
    

    void Start()
    {
        
    }

    void Update()
    {
        if(Keyboard.current.wKey.isPressed == true)
        {
           transform.Translate(transform.forward * 5 * Time.deltaTime);
           
        }

        if(Keyboard.current.sKey.isPressed == true)
        {
           transform.Rotate(transform.forward * -5 * Time.deltaTime);
        }

        if(Keyboard.current.aKey.isPressed == true)
        {
           transform.Rotate(Vector3.up * -90 * Time.deltaTime);
        }

        if(Keyboard.current.dKey.isPressed == true)
        {
            transform.Rotate(Vector3.up * 90 * Time.deltaTime);
        }

        //マウスの移動量の取得
        Vector2 mouseDelta = Mouse.current.delta.ReadValue();
        Debug.Log($"マウスの移動量: {mouseDelta}");

        //角度の増減の計算
        topAngles.y += mouseDelta.x * 0.1f;                         //上部ジョイントの角度を更新
        cannonAngles.x -= mouseDelta.y * 0.1f;                      //砲身の角度を更新
        cannonAngles.x = Mathf.Clamp(cannonAngles.x, -10f, 30f);    //砲身の角度を制限

        //各ジョイントに角度を反映
        topJoint.localEulerAngles = topAngles;
        cannonJoint.localEulerAngles = cannonAngles;


        //弾丸発射
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
           //弾丸の生成
          　GameObject bullet = Instantiate(bulletPrefabs, shotPoint.position, shotPoint.rotation);
            bullet.GetComponent<Rigidbody>()
                .AddForce(shotPoint.forward * 25f, ForceMode.Impulse);

            Destroy(bullet,5f); //5秒後に弾丸を破棄
        }
    }  
}
