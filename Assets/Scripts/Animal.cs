using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private SpriteRenderer spriteRenderer;

    [Header("Data")]
    [SerializeField] private AnimalType animalType;
    private bool hasCollided;

    [Header("Actions")]
    public static Action<Animal, Animal> onCollisionWithAnimal;

    public void MoveTo(Vector2 targetPosition)
    {
        gameObject.transform.position = targetPosition;
    }

    public void EnablePhysics()
    {
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        gameObject.GetComponent<Collider2D>().enabled = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        hasCollided = true;

        if (collision.collider.TryGetComponent(out Animal otherAnimal))
        {
            if (otherAnimal.GetAnimalType() != animalType)
            {
                return;
            }

            onCollisionWithAnimal?.Invoke(this, otherAnimal);
        }
    }

    public AnimalType GetAnimalType()
    {
        return animalType;
    }

    public Sprite GetSprite()
    {
        return spriteRenderer.sprite;
    }

    public Color GetColor()
    {
        return spriteRenderer.color;
    }

    public bool HasCollided()
    {
        return hasCollided;
    }
}
