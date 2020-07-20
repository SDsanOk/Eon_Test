using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class BonusTestScript
    {
        private Bonus _bonus;
        private Tank _tank;

        [SetUp]
        public void Setup()
        {
            GameObject bonusGameObject = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Bonus"));
            GameObject tankGameObject = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Tank"));

            _bonus = bonusGameObject.GetComponent<Bonus>();

            _tank = tankGameObject.GetComponent<Tank>();
            _tank.PlayerNumber = 1;
        }

        [TearDown]
        public void Teardown()
        {
            Object.Destroy(_tank.gameObject);
        }

        [Test]
        public void BonusTestScript_Success()
        {
            //Не знаю на что писать тесты. Т.к. в юнити немного другой подход, нежели в .net core, то по сути и подмокать ничего нельзя.
            //Все на статике либо конкретных префабах и взаимодействии компонентов обьектов. Единственный подход - это Play тесты.
        }

        [UnityTest]
        public IEnumerator BonusAppliesCorrectEffect()
        {
            _bonus.SetBonusType(BonusType.Armor);
            var tankHpComponent = _tank.gameObject.GetComponent<Hp>();
            var tankHpBeforePickingUpBonus = tankHpComponent.GetHp();
            _bonus.transform.position = _tank.transform.position;

            yield return new WaitForSeconds(0.1f);

            var tankHpAfterPickingUpBonus = tankHpComponent.GetHp();

            Assert.IsNull(_bonus);
            Assert.AreEqual(1 ,tankHpBeforePickingUpBonus);
            Assert.AreEqual(GameConstants.HpValueAfterPickingUpArmorBonus, tankHpAfterPickingUpBonus);
        }
    }
}
