using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockField_1 : MonoBehaviour
{
    // ブロックの種類
    enum Block
    {
        None,
        Floor,
        Wall,
        Goal,
        Lanthanum,

        Max
    }
    [SerializeField] private GameObject[] _prefab = null;
    // 生成するブロックのサイズ（値と二次元配列の数を合わせる）
    const int FIELD_SIZE_X = 31;
    const int FIELD_SIZE_Y = 18;
    static readonly int[,] FIELD = new int[,]
    {
        { 2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,},
        { 2,0,0,2,0,0,2,2,2,2,2,0,0,0,0,0,0,0,2,2,2,2,2,2,2,2,2,2,2,2,2,},
        { 2,0,0,2,0,0,2,2,2,2,2,0,0,0,0,0,0,0,2,2,2,2,2,2,2,2,2,2,2,2,2,},
        { 2,0,0,2,0,0,2,2,2,2,2,0,0,2,2,2,0,0,2,2,2,2,2,2,2,2,2,2,2,2,2,},
        { 2,0,0,2,0,0,2,2,2,2,2,0,0,2,2,2,0,0,2,2,2,2,2,0,2,2,2,2,2,2,2,},
        { 2,0,0,2,0,0,0,0,0,0,0,0,0,2,2,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,},
        { 2,0,0,2,0,0,0,0,0,0,0,0,0,2,2,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,},
        { 2,0,0,2,0,0,2,2,2,2,2,0,0,2,2,2,0,0,2,2,2,2,2,2,2,2,2,2,0,0,2,},
        { 2,0,0,2,0,0,2,2,2,2,2,0,0,2,2,2,0,0,2,2,2,2,2,2,2,2,2,2,0,0,2,},
        { 2,0,0,2,0,0,2,2,2,2,2,0,0,2,2,2,0,0,2,2,2,2,2,2,2,2,2,2,0,0,2,},
        { 2,0,0,0,0,0,2,2,2,2,2,0,0,0,0,0,0,0,0,0,0,0,0,0,2,2,2,2,0,0,2,},
        { 2,0,0,0,0,0,2,2,2,2,2,0,0,0,0,0,0,0,0,0,0,0,0,0,2,2,2,2,0,0,2,},
        { 2,0,0,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,0,0,2,0,0,0,0,0,0,0,0,2,},
        { 2,0,0,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,0,0,2,0,0,0,0,0,0,0,0,2,},
        { 2,0,0,2,2,2,2,2,2,2,2,0,0,0,2,2,2,2,2,0,0,2,2,2,2,2,2,2,2,2,2,},
        { 2,0,0,0,0,0,2,2,2,2,2,0,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,},
        { 2,0,0,0,0,0,2,2,2,2,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,},
        { 2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,},
    };
    

    // Start is called before the first frame update
    void Start()
    {
        // フィールドブロックを生成

        // 左下が 0,0 の位置に生成されるため真ん中までの距離を足す
        float ofsX = -(FIELD_SIZE_X - 1) * 0.5f;
        float ofsY = -(FIELD_SIZE_Y - 1) * 0.5f;

        for (int y = 0; y < FIELD_SIZE_Y; ++y)
        {
            for(int x = 0; x < FIELD_SIZE_X; ++x)
            {
                // 床のオブジェクトを生成する
                GameObject floorObj = Instantiate<GameObject>(_prefab[(int)Block.Floor]);
                // 座標を１マス分下にずらす
                floorObj.transform.localPosition = new Vector3(x + ofsX, -0.5f, y + ofsY);

                // 何のオブジェクトかを判別するための変数
                int id = FIELD[y, x];
                // 0ならこれ以降の処理を無視する
                if(id == 0)
                {
                    continue;
                }
                // _prefabのy,x番目の値のprefabを生成する
                GameObject newObj = Instantiate<GameObject>(_prefab[FIELD[y, x]]);

                // 生成される座標を指定する（0,0の位置に生成されるようにする）
                // new Vector3のYにyの値を入れると縦に生成されるため、Z座標に入れる
                newObj.transform.localPosition = new Vector3(x + ofsX, 0.5f, y + ofsY);
            }
        }
    }
}
