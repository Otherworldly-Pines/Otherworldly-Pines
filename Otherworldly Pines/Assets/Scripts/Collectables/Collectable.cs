using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
   public Item item;

   public Item getItem()
   {
      return item;
   }

   public void setItem(Item i)
   {
      item = i;
   }
   public enum Item
   {
      Apple,
      Banana,
      Cookie,
      Cupcake,
      Pie,
      Water
   }
}
