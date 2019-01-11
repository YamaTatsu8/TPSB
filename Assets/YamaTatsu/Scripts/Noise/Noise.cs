using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noise : MonoBehaviour {

    //シェーダー
    Shader _shader;

    //マテリアル
    Material _mat;

    //範囲0~1
    [Range(0, 1)]
    public float horizonValue;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if(_mat == null)
        {
            _shader = Shader.Find("Hidden/Noise2");
            _mat = new Material(_shader);
            _mat.hideFlags = HideFlags.DontSave;
        }

        //ランダムシード値を更新することで乱数を動かす
        _mat.SetFloat("_HorizonValue", horizonValue);
        Graphics.Blit(source, destination, _mat);

    }
}
