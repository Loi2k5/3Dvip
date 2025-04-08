using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using TMPro;

public class NewTestScript
{
    // 1. Kiểm tra hiển thị điểm ban đầu trên highScoreText
    [Test]
    public void HienThiDiemBanDau()
    {
        GameObject obj = new GameObject();
        Raycast raycast = obj.AddComponent<Raycast>();

        GameObject textObj = new GameObject();
        var scoreText = textObj.AddComponent<TextMeshProUGUI>();
        raycast.highScoreText = scoreText;

        raycast.highScore = 0;
        raycast.UpdateHighScoreText();

        Assert.AreEqual("High Score: 0", scoreText.text);
    }

    // 2. Kiểm tra cộng điểm và cập nhật điểm cao nhất
    [Test]
    public void CapNhatDiemKhiNhanXu()
    {
        GameObject obj = new GameObject();
        Raycast raycast = obj.AddComponent<Raycast>();

        GameObject textObj = new GameObject();
        var scoreText = textObj.AddComponent<TextMeshProUGUI>();
        raycast.highScoreText = scoreText;

        raycast.highScore = 0;
        raycast.AddScore(10);

        Assert.AreEqual("High Score: 10", scoreText.text);
    }

    // 3. Kiểm tra lưu điểm cao nhất vào PlayerPrefs
    [Test]
    public void LuuDiemCaoNhat()
    {
        GameObject obj = new GameObject();
        Raycast raycast = obj.AddComponent<Raycast>();

        raycast.highScore = 50;
        raycast.SaveHighScore();

        Assert.AreEqual(50, PlayerPrefs.GetInt("HighScore"));
    }

    // 4. Kiểm tra load điểm cao nhất từ PlayerPrefs
    [Test]
    public void LoadDiemCaoNhat()
    {
        PlayerPrefs.SetInt("HighScore", 30);
        PlayerPrefs.Save();

        GameObject obj = new GameObject();
        Raycast raycast = obj.AddComponent<Raycast>();
        raycast.LoadHighScore();

        Assert.AreEqual(30, raycast.highScore);
    }

    // 5. Kiểm tra reset điểm cao nhất //UGUI
    [Test]
    public void ResetDiemCaoNhat()
    {
        PlayerPrefs.SetInt("HighScore", 99);
        PlayerPrefs.Save();

        GameObject obj = new GameObject();
        Raycast raycast = obj.AddComponent<Raycast>();
        raycast.ResetHighScore();

        Assert.AreEqual(0, raycast.highScore);
        Assert.AreEqual(0, PlayerPrefs.GetInt("HighScore"));
    }

    // 6. Kiểm tra cập nhật số xu nhặt được //UGUI
    [Test]
    public void CapNhatSoXuNhanDuoc()
    {
        GameObject obj = new GameObject();
        Raycast raycast = obj.AddComponent<Raycast>();

        GameObject textObj = new GameObject();
        var countText = textObj.AddComponent<TextMeshProUGUI>();
        raycast.destroyedCountText = countText;

        raycast.destroyedCount = 5;
        raycast.UpdateDestroyedCountText();

        Assert.AreEqual("Loot: 5", countText.text);
    }

    // 7. Kiểm tra raycast trúng mục tiêu
    [Test]
    public void RaycastTrungMucTieu()
    {
        GameObject raycastObject = new GameObject("RaycastObject");
        Raycast raycast = raycastObject.AddComponent<Raycast>();

        GameObject target = GameObject.CreatePrimitive(PrimitiveType.Cube);
        target.transform.position = raycastObject.transform.position + raycastObject.transform.forward * 3;

        int testLayer = 8;
        target.layer = testLayer;
        raycastObject.transform.position = Vector3.zero;

        raycast.GetType()
            .GetField("layerMask", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .SetValue(raycast, LayerMask.GetMask(LayerMask.LayerToName(testLayer)));

        bool hit = raycast.PerformRaycast();

        Assert.IsTrue(hit, "Raycast không trúng mục tiêu!");
    }

    // 8. Kiểm tra raycast bằng coroutine (UnityTest)
    [UnityTest]
    public IEnumerator RaycastCoroutineTrungMucTieu()
    {
        GameObject raycastObject = new GameObject("RaycastObject");
        Raycast raycast = raycastObject.AddComponent<Raycast>();
        yield return null;

        GameObject target = GameObject.CreatePrimitive(PrimitiveType.Cube);
        target.transform.position = raycastObject.transform.position + raycastObject.transform.forward * 3;

        int testLayer = 8;
        target.layer = testLayer;
        raycastObject.transform.position = Vector3.zero;

        raycast.GetType()
            .GetField("layerMask", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .SetValue(raycast, LayerMask.GetMask(LayerMask.LayerToName(testLayer)));

        bool hit = raycast.PerformRaycast();

        Assert.IsTrue(hit, "Raycast không trúng mục tiêu!");
    }

}
