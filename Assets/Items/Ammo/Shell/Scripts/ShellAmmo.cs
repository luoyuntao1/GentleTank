using System.Collections;
using UnityEngine;

namespace Item.Ammo
{
    public class ShellAmmo : AmmoBase
    {
        public new Rigidbody rigidbody;                     // �Լ��ĸ���
        public ObjectPool shellExplosionPool;               // ��ը���Գ�
        public float explosionForce = 100f;                 // ��ը���ĵ�����
        public float explosionRadius = 5f;                  // ��ը�뾶

        // ��ÿ����Ҫ�õ�����ʱ����������
        private Collider[] colliders;                       // ��ײ������
        private Rigidbody targetRigidbody;                  // Ŀ�����
        private HealthManager targetHealth;                 // Ŀ��Ѫ��

        private void OnDisable()
        {
            rigidbody.Sleep();
        }

        protected override IEnumerator OnCollision(Collision other)
        {
            // �ӱ�ը���л�ȡ���󣬲�����λ�ã���ʾ֮
            shellExplosionPool.GetNextObject(transform: transform);

            // ��ȡ��ը��Χ��������ײ��
            colliders = Physics.OverlapSphere(transform.position, explosionRadius);

            for (int i = 0; i < colliders.Length; i++)
            {
                targetRigidbody = colliders[i].GetComponent<Rigidbody>();
                //AddForce(colliders[i]);
                TakeDamage(colliders[i]);
            }
            yield return null;
        }

        /// <summary>
        /// ��һ����ը��,��AI��Ч��NavMeshAgent������ʱ����������䲻��ʵ�֣�
        /// </summary>
        /// <param name="collider"></param>
        private void AddForce(Collider collider)
        {
            if (!targetRigidbody)
                return;
            targetRigidbody.AddExplosionForce(explosionForce, transform.position, explosionRadius);
        }

        /// <summary>
        /// ��ȡĿ���Ѫ���������Ѫ����������Ѫ��
        /// </summary>
        /// <param name="collider"></param>
        private void TakeDamage(Collider collider)
        {
            targetHealth = collider.GetComponent<HealthManager>();
            if (!targetHealth)
                return;
            targetHealth.SetHealthAmount(-1 * CalculateDamage(collider.transform.position));
        }

        /// <summary>
        /// ���ݾ�������˺�ֵ
        /// </summary>
        /// <param name="targetPosition">Ŀ��λ��</param>
        /// <returns></returns>
        private float CalculateDamage(Vector3 targetPosition)
        {
            // ���㱬ը���ľ�����Լ��ľ���
            Vector3 explosionToTarget = targetPosition - transform.position;
            float explosionDistance = explosionToTarget.magnitude;

            // ת���ɱ���
            float relativeDistance = (explosionRadius - explosionDistance) / explosionRadius;

            // ���ݱ��������˺�
            return Mathf.Max(0f, relativeDistance * damage);
        }

    }
}