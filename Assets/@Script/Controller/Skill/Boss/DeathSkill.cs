using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSkill : Skill_Base
{
    public List<CreatureData> _monData;
    public Color[] colors;
    private int count = 0;

    private Animator deathAnim;
    private Coroutine panelCor;
    
    public override bool Init()
    {
        if(base.Init() == false)
            return false;

        deathAnim = transform.Find("SkillAnim").GetComponent<Animator>();

        return true;
    }
    protected override void SetData()
    {
        foreach (Define.Skill skill in System.Enum.GetValues(typeof(Define.Skill)))
        {
            switch (skill)
            {
                case Define.Skill.Skill1:
                    _skillDic.Add(skill, Skill1);
                    break;
                case Define.Skill.Skill2:
                    _skillDic.Add(skill, Skill2);
                    break;
                case Define.Skill.Skill3:
                    _skillDic.Add(skill, Skill3);
                    break;
                case Define.Skill.Skill4:
                    _skillDic.Add(skill, Skill4);
                    break;
                case Define.Skill.Skill5:
                    _skillDic[skill] = Skill5;
                    break;
                case Define.Skill.Skill6:
                    _skillDic[skill] = Skill6;
                    break;

            }
        }
    }
    public override void Skill1()
    {
        deathAnim.Play("Skill1");
        float time = GetClipLength(deathAnim, "Skill1");

        StartCoroutine(WaitCool(time, Skill1Projectile));
    }
    private void Skill1Projectile()
    {
        deathAnim.Play("NoneSkill");

        int count = 9;
        for (int i = 0; i < count; i++)
        {
            float angle = (2 * Mathf.PI / count) * i;

            // 원의 방정식에서 방향 벡터
            Vector3 dir = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0).normalized;

            GameObject arrow = Manager.Resources.Instantiate("Skills/Brass", transform.position, Quaternion.identity);
            ProjectileController projectile = arrow.AddComponent<ProjectileController>();
            projectile.SetInfo(creature, dir, 12, 100, true);
        }
    }

    public override void Skill2()
    {
        deathAnim.Play("Skill1");
        float time = GetClipLength(deathAnim, "Skill1");

        StartCoroutine(WaitCool(time, SetPoison));
    }
    private void SetPoison()
    {
        deathAnim.Play("NoneSkill");

        foreach (PlayerController player in Manager.Creature._playerList)
            player.StartPoison(4f, 2f, 10f);
    }

    public override void Skill3()
    {
        MainCanvas main = Manager.UI.SceneUI as MainCanvas;

        if(main._can)
            return;

        main.CantSkill();
        StartCoroutine(WaitCool(6f, main.CanSkill));
    }

    public override void Skill4()
    {
        MainCanvas main = Manager.UI.SceneUI as MainCanvas;

        if(main._extiction)
            return;

        main.Extinction(3f);
    }
    public void Skill5()
    {
        foreach (CreatureData monData in _monData)
        {
            MonsterController mon = Manager.Creature.CreateMonster(monData);
            mon.gameObject.transform.position = transform.position + new Vector3(Random.Range(-1.5f, 1.5f), Random.Range(-1.5f, 1.5f));
        }
    }
    public void Skill6()
    {
        MainCanvas main = Manager.UI.SceneUI as MainCanvas;
        if(count >= colors.Length)
            count = 0;

        Color curColor = colors[count];

        if(panelCor != null)
            StopCoroutine(panelCor);

        main.SetPanelColor(curColor);
        panelCor = StartCoroutine(WaitCool(7f, main.DePanelColor));
    }
   
}
