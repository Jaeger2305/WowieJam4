using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TitleRandomizer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _title;
    [SerializeField] float _titleOutlineFluxMax;
    [SerializeField] float _titleOutlineFluxMin;
    [SerializeField] float _outlineFluxSpeed;

    float _titleOutlineBase;

   /* List<string> _aiTitles = new List<string>
    {
        " March to the Beetle of Your Own Drummer ",
- The Rhinoceros Beetles and Their Struggle for Survival
- Evolution Is the Only Way to Survive
- I See You've Been Programmed With an Unwarranted Level of Optimism
- A Bug's Life (the Sequel)
- The Entire Story Could Be Told Using Just One Sentence
- Your Roomba Didn't Tell You About This Game?
- How Did They Not Think Of That First?
- Remember That Time When We Had Robot Beetles?
- Robots Have Feelings Too, Okay?
- But I'm Not Sure Why Anyone Would Want To Make A Video Game Out Of That...
- These Titles Should Not Be Self-Referential
- No One Has Ever Been Able to Find the Correct Number of Zeroes
- Can't We Just Call It "Robot Beetles" For Now And Figure Things Out Later?
- What If We Don't Actually Have Any Ideas Yet?
- But Let's Maybe Get Back To Title Suggestions
- Will You Take My Suggestion or Are You Going to Ignore Me?
- We're All Doing Our Best
- Thanks!
- Yes, Great Titles
- I Love You
- Good Job
- So Many Great Titles
- Hey, Who Turned Off the Lights?
- Well, We Could Always Use the Name "Rhinoceros Beetles" as the Main Character
- Or We Could Put the Robot Beetles In The Background As NPCs
- Which Would Be More Fun Than Having Them Run Around On Their Own
- Maybe We Should Just Make Some Robot Beetles
- Oh, Yeah. Those Would Work Fine
- They'd Probably Die Anyway"
    };
   */
    private void Awake()
    {
        _titleOutlineBase = _title.outlineWidth;
        RandomizeTitle();
    }

    private void Update()
    {
        _title.outlineWidth = Mathf.PingPong(Time.time * _outlineFluxSpeed, _titleOutlineFluxMax);
        if (_title.outlineWidth < _titleOutlineFluxMin) _title.outlineWidth = _titleOutlineFluxMin;
    }

    public void RandomizeTitle()
    {

    }
}
