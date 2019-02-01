using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(WeaponManager))]

[CanEditMultipleObjects]
public class WeaponManagerEditor : Editor
{

    public override void OnInspectorGUI()
    {
        WeaponManager weapon = target as WeaponManager;

        //入力された弾の種類を設定
        weapon._type = (BulletType)EditorGUILayout.EnumPopup("弾の種類", weapon._type);

        //各タイプ毎に注記を表示
        if (weapon._type != BulletType.Heal)
        {
            EditorGUILayout.HelpBox("RayCastShootを同じオブジェクトに \nアタッチしてください。", MessageType.Info, true);
        }
        if (weapon._type != BulletType.Normal)
        {
            EditorGUILayout.HelpBox("射撃モード、1秒間に撃てる数は設定できません", MessageType.Info, true);
        }

        //共通の注記
        EditorGUILayout.HelpBox("装弾数、弾速、リロードにかかる時間は \n0に出来ません。", MessageType.Info, true);

        //どのタイプでも設定出来るもの
        weapon._capacity = Mathf.Max(1, EditorGUILayout.IntField("装弾数", weapon._capacity));

        weapon._reloadTime = Mathf.Max(1, EditorGUILayout.FloatField("リロードにかかる時間", weapon._reloadTime));

        if (weapon._type != BulletType.Heal)
        {
            weapon._bulletSpeed = Mathf.Max(1, EditorGUILayout.FloatField("弾速", weapon._bulletSpeed));
        }

        weapon._bulletPrefab = EditorGUILayout.ObjectField("弾のプレハブ", weapon._bulletPrefab, typeof(GameObject), false) as GameObject;

        weapon._seName = EditorGUILayout.TextField("発射音(ファイル名)", weapon._seName);

        if (weapon._type == BulletType.Normal)
        {
            weapon._roundsPerSecond = Mathf.Max(1, EditorGUILayout.IntField("1秒間に撃てる数  ", weapon._roundsPerSecond));

            weapon._mode = (WeaponManager.Selector)EditorGUILayout.EnumPopup("射撃モード", weapon._mode);
        }

        EditorUtility.SetDirty(target);
    }

}