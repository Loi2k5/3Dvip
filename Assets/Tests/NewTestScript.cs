using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using TMPro;

public class NewTestScript
{
    [Test]
    public void TestRaycastHit()
    {
        GameObject raycastObject = new GameObject();
        Raycast raycast = raycastObject.AddComponent<Raycast>();

        // Tạo một vật thể làm mục tiêu của Raycast
        GameObject target = GameObject.CreatePrimitive(PrimitiveType.Cube);
        target.transform.position = new Vector3(0, 0, 5); // Đặt xa một chút để raycast có thể bắn tới

        // Gọi hàm Raycast trong script
        bool hit = raycast.PerformRaycast();

        Assert.IsTrue(hit, "Raycast không trúng mục tiêu!");
    }

    [Test]
    public void TestUpdateHighScore()
    {
        GameObject gameObject = new GameObject();
        Raycast rayCast = gameObject.AddComponent<Raycast>();

        // Tạo TextMeshProUGUI giả lập
        var textObject = new GameObject();
        var textComponent = textObject.AddComponent<TextMeshProUGUI>();
        rayCast.highScoreText = textComponent;

        // Gọi hàm cập nhật điểm
        rayCast.UpdateHighScoreText();

        // Kiểm tra kết quả
        Assert.AreEqual("High Score: 0", textComponent.text);
    }

    [Test]
    public void TestSaveHighScore()
    {
        GameObject gameObject = new GameObject();
        Raycast rayCast = gameObject.AddComponent<Raycast>();

        // Gán giá trị highScore
        rayCast.highScore = 10;
        rayCast.SaveHighScore();

        Assert.AreEqual(10, PlayerPrefs.GetInt("HighScore"));
    }

    [Test]
    public void TestLoadHighScore()
    {
        PlayerPrefs.SetInt("HighScore", 15);
        PlayerPrefs.Save();

        GameObject gameObject = new GameObject();
        Raycast rayCast = gameObject.AddComponent<Raycast>();
        rayCast.LoadHighScore();

        Assert.AreEqual(15, rayCast.highScore);
    }

    [Test]
    public void TestResetHighScore()
    {
        PlayerPrefs.SetInt("HighScore", 20);
        PlayerPrefs.Save();

        GameObject gameObject = new GameObject();
        Raycast rayCast = gameObject.AddComponent<Raycast>();
        rayCast.ResetHighScore();

        Assert.AreEqual(0, rayCast.highScore);
        Assert.AreEqual(0, PlayerPrefs.GetInt("HighScore"));
    }

    [Test]
    public void TestUpdateDestroyedCountText()
    {
        GameObject gameObject = new GameObject();
        Raycast rayCast = gameObject.AddComponent<Raycast>();

        var textObject = new GameObject();
        var textComponent = textObject.AddComponent<TextMeshProUGUI>();
        rayCast.destroyedCountText = textComponent;
        rayCast.destroyedCount = 5;

        rayCast.UpdateDestroyedCountText();

        Assert.AreEqual("Loot: 5", textComponent.text);
    }

    [UnityTest]
    public IEnumerator TestRaycastCoroutine()
    {
        GameObject raycastObject = new GameObject();
        Raycast raycast = raycastObject.AddComponent<Raycast>();
        yield return null;

        bool hit = raycast.PerformRaycast();
        Assert.IsTrue(hit, "Raycast không trúng mục tiêu!");
    }
}
