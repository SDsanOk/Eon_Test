using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class BulletTestScript
    {
        private Bullet _bullet;
        private Tank _tank;

        [SetUp]
        public void Setup()
        {
            GameObject bulletGameObject = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Bullet"));
            GameObject tankGameObject = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Tank"));

            _bullet = bulletGameObject.GetComponent<Bullet>();

            _tank = tankGameObject.GetComponent<Tank>();
            _tank.PlayerNumber = 1;
        }

        [TearDown]
        public void Teardown()
        {
        }


        [UnityTest]
        public IEnumerator BulletDestroysTank()
        {
            var tankHpComponent = _tank.gameObject.GetComponent<Hp>();
            var tankHpBeforeShoot = tankHpComponent.GetHp();
            _bullet.transform.position = _tank.transform.position;

            yield return new WaitForSeconds(0.1f);

            Assert.True(_bullet == null);
            Assert.True(_tank == null);
        }
    }
}