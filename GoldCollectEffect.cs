namespace VeryDisco.Common
{
    using System.Collections;
    using UnityEngine;
    using UnityEngine.Pool;
    using DG.Tweening;

    /// <summary>
    /// Very simple and popular effect mobile game
    /// Author: Tran Nhat Linh - nhatlinh.nh2511@gmail.com
    /// </summary>
    public class GoldCollectEffect : GenericSingleton<GoldCollectEffect>
    {
        [SerializeField] private GameObject goldItemPrefab;
        [SerializeField] private float spawnRadiusMin = 1f;
        [SerializeField] private float spawnRadiusMax = 3f;
        [SerializeField] private float moveOutDuraion = 0.3f;
        [SerializeField] private float moveDuration = 1f;
        [SerializeField] private float spawnDelay = 0.05f;
        [SerializeField] private AnimationCurve moveOutAnim;
        [SerializeField] private AnimationCurve moveAnim;
        
        private Vector3 startPos, endPos;
        private ObjectPool<GameObject> goldItemPool;

        private void Start()
        {
            goldItemPool = new ObjectPool<GameObject>(() => Instantiate(goldItemPrefab, transform));
        }

        public void PlayGoldEffect(Vector3 worldPos, Vector3 uiPos, int amount)
        {
            startPos = worldPos;
            endPos = uiPos;

            StartCoroutine(PlayGoldEffectJob(amount));
        }

        IEnumerator PlayGoldEffectJob(int amount)
        {
            for(int i = 0; i < amount; i++)
            {
                GameObject goldObj = goldItemPool.Get();
                goldObj.gameObject.SetActive(true);
                goldObj.transform.position = startPos;

                Vector3 spawnPos = startPos + (Vector3)(Random.insideUnitCircle * Random.Range(spawnRadiusMin, spawnRadiusMax));

                goldObj.transform.DOScale(1f, moveOutDuraion);

                goldObj.transform.DOMove(spawnPos, moveOutDuraion)
                    .SetEase(moveOutAnim)
                    .OnComplete(() =>
                    {
                        goldObj.transform.DOMove(endPos, moveDuration)
                            .SetEase(moveAnim)
                            .OnComplete(() =>
                            {
                                goldObj.SetActive(false);
                                goldObj.transform.localScale = Vector3.zero;
                                goldItemPool.Release(goldObj);
                            });
                    });

                yield return new WaitForSeconds(spawnDelay);
            }
        }
    }
}
