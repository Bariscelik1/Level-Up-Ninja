using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed;
    public Animator animator;
    public GameManager gameManager;
    public CameraController cameraController;
    public GameObject[] lighntingFX;
    int lighntingFXIndex;
    public Material enemyDead;



    public Touch touch;
    public float speedModifier;
    public bool isEnd;

    // Start is called before the first frame update
    void Start()
    {       
        speedModifier = 0.125f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isEnd)
        {
            gameObject.transform.Translate(Vector3.forward * speed * Time.deltaTime);

            if (Input.touchCount > 0)
            {
                touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Moved)
                {
                    transform.position = (new Vector3(transform.position.x + touch.deltaPosition.x * speedModifier * Time.deltaTime, transform.position.y, transform.position.z));
                }
            }
        }
        
        
       

    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Enemy"))
        {
            if (int.Parse(other.name) <= gameManager.playerLevel)
            {
                animator.SetTrigger("Attack 3");

                other.GetComponentInChildren<SkinnedMeshRenderer>().material = enemyDead;

                other.GetComponent<Rigidbody>().AddForce(new Vector3(0, 1, 0.3f) * 100);

                try
                {
                    other.GetComponent<Animator>().SetTrigger("isDead");
                }
                catch
                {

                    
                }

                
                
                other.GetComponent<CapsuleCollider>().enabled = false;

                gameManager.sounds[gameManager.soundIndex].Play();
                gameManager.soundIndex++;

                if (gameManager.soundIndex == gameManager.sounds.Count - 1)
                {
                    gameManager.soundIndex = 0;
                }


                gameManager.playerLevel += int.Parse(other.name);
                gameManager.diamondChange();

                gameManager.playerLevel_textChange();
            }
            else
            {

                gameManager.Lose();
            }

            if (int.Parse(other.name)>1)
            {
                lighntingFX[lighntingFXIndex].GetComponent<ParticleSystem>().Play();
                lighntingFXIndex++;

                if (lighntingFXIndex == lighntingFX.Length - 1)
                {
                    lighntingFXIndex = 0;
                }
            }

            


        }
        if (other.gameObject.CompareTag("Fire"))
        {
            gameManager.playerLevel += int.Parse(other.name);
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
            if (gameManager.playerLevel >= 0)
            {
                
                gameManager.hurtSound.Play();
                gameManager.playerLevel_textChange();
            }
            if (gameManager.playerLevel <= 0)
            {
                gameManager.playerLevel = 1;
                gameManager.playerLevel_textChange();
                gameManager.Lose();
            }

        }
        if (other.gameObject.CompareTag("Saw"))
        {
            gameManager.Lose();
        }

        if (other.gameObject.CompareTag("Lava"))
        {
            gameManager.hurtSound.Play();
            gameManager.playerLevel -= int.Parse(other.name);
            other.gameObject.GetComponent<CapsuleCollider>().enabled = false;
            if (gameManager.playerLevel >= 0)
            {
                                
                gameManager.playerLevel_textChange();
            }
            if (gameManager.playerLevel <= 0)
            {
                gameManager.playerLevel = 1;
                gameManager.playerLevel_textChange();
                
            }
        }

        if (other.gameObject.CompareTag("End_area"))
        {            
            cameraController.endArea = true;
            StartCoroutine(Win());
        }
        if (other.gameObject.CompareTag("Stop_area"))
        {
            isEnd = true;
            animator.SetBool("isEnd", true);
        }
        if (other.gameObject.CompareTag("Diamond"))
        {
            other.gameObject.SetActive(false);

            gameManager.Gemsounds[gameManager.gemSoundIndex].Play();
            gameManager.gemSoundIndex++;

            if (gameManager.gemSoundIndex == gameManager.Gemsounds.Count - 1)
            {
                gameManager.gemSoundIndex = 0;
            }

            gameManager.diamondCount++;
            gameManager.diamondChange();
            
        }


        IEnumerator Win()
        {
            yield return new WaitForSeconds(2);            
            gameManager.Win();
        }

    }





}
