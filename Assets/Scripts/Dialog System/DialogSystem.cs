using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DialogSystem : MonoBehaviour
{
	[HideInInspector]
	public static DialogSystem instance;
	[SerializeField]
	private string branch;
	[SerializeField]
	private DialogDB dialogDB;
	[SerializeField]
	private	Speaker[]		speakers;                   // ��ȭ�� �����ϴ� ĳ���͵��� UI �迭
    [SerializeField]
    private MapProfile[] mapProfiles;                 // ��ȭ�� �����ϴ� ĳ���͵��� UI �迭
    [SerializeField]
	private	DialogData[]	dialogs;                    // ���� �б��� ��� ��� �迭
	[SerializeField]
	private Image blockImage;
	public bool isDialogSystemEnded;
	[SerializeField]
	private	bool			isAutoStart = true;			// �ڵ� ���� ����
	private	bool			isFirst = true;				// ���� 1ȸ�� ȣ���ϱ� ���� ����
	private	int				currentDialogIndex = -1;	// ���� ��� ����
	private	int				currentSpeakerIndex = 0;	// ���� ���� �ϴ� ȭ��(Speaker)�� speakers �迭 ����
	private	float			typingSpeed = 0.1f;			// �ؽ�Ʈ Ÿ���� ȿ���� ��� �ӵ�
	private	bool			isTypingEffect = false;		// �ؽ�Ʈ Ÿ���� ȿ���� ���������

	private void Awake()
	{
        instance = this;
	}

    private void Start()
    {
        Setup();
    }

    private void Setup()
	{
		branch = string.Empty;
		branch = $"STAGE_{MainManager.instance.StageIndex + 1}";

		if (MainManager.instance.StageIndex >= 0 && MainManager.instance.StageIndex <= 4) 
		{
			speakers[1].textName.text = mapProfiles[0].NPCName;
			speakers[1].characterImage.sprite = mapProfiles[0].NPCSprite;
		}
		else if (MainManager.instance.StageIndex >= 5 && MainManager.instance.StageIndex <= 9)
        {
            speakers[1].textName.text = mapProfiles[1].NPCName;
            speakers[1].characterImage.sprite = mapProfiles[1].NPCSprite;
        }
		else if (MainManager.instance.StageIndex >= 10 && MainManager.instance.StageIndex <= 14)
        {
            speakers[1].textName.text = mapProfiles[2].NPCName;
            speakers[1].characterImage.sprite = mapProfiles[2].NPCSprite;
        }

		int index = 0;
		for (int i = 0; i < dialogDB.Entites.Count; i++)
		{
			if (dialogDB.Entites[i].branch == branch)
			{
				dialogs[index].name = dialogDB.Entites[i].name;
				dialogs[index].dialogue = dialogDB.Entites[i].dialog;
				index++;
			}
		}

		// ��� ��ȭ ���� ���ӿ�����Ʈ ��Ȱ��ȭ
		for ( int i = 0; i < speakers.Length; ++ i )
		{
			SetActiveObjects(speakers[i], false);
			// ĳ���� �̹����� ���̵��� ����
			speakers[i].characterImage.gameObject.SetActive(true);
		}
	}

	public bool UpdateDialog()
	{
		// ��� �бⰡ ���۵� �� 1ȸ�� ȣ��
		if ( isFirst == true )
		{
			// �ʱ�ȭ. ĳ���� �̹����� Ȱ��ȭ�ϰ�, ��� ���� UI�� ��� ��Ȱ��ȭ
			Setup();

			// �ڵ� ���(isAutoStart=true)���� �����Ǿ� ������ ù ��° ��� ���
			if ( isAutoStart ) SetNextDialog();

			isFirst = false;
		}

		if ( Input.GetMouseButtonDown(0) )
		{
			// �ؽ�Ʈ Ÿ���� ȿ���� ������϶� ���콺 ���� Ŭ���ϸ� Ÿ���� ȿ�� ����
			if ( isTypingEffect == true )
			{
				isTypingEffect = false;
				
				// Ÿ���� ȿ���� �����ϰ�, ���� ��� ��ü�� ����Ѵ�
				StopCoroutine("OnTypingText");
				speakers[currentSpeakerIndex].textDialogue.text = dialogs[currentDialogIndex].dialogue;
				// ��簡 �Ϸ�Ǿ��� �� ��µǴ� Ŀ�� Ȱ��ȭ
				speakers[currentSpeakerIndex].objectArrow.SetActive(true);

				return false;
			}

			// ��簡 �������� ��� ���� ��� ����
			if ( dialogs.Length > currentDialogIndex + 1 )
			{
				SetNextDialog();
			}
			// ��簡 �� �̻� ���� ��� ��� ������Ʈ�� ��Ȱ��ȭ�ϰ� true ��ȯ
			else
			{
				// ���� ��ȭ�� �����ߴ� ��� ĳ����, ��ȭ ���� UI�� ������ �ʰ� ��Ȱ��ȭ
				for ( int i = 0; i < speakers.Length; ++ i )
				{
					SetActiveObjects(speakers[i], false);
					// SetActiveObjects()�� ĳ���� �̹����� ������ �ʰ� �ϴ� �κ��� ���� ������ ������ ȣ��
					speakers[i].characterImage.gameObject.SetActive(false);
				}

				return true;
			}
		}

		return false;
	}

	private void SetNextDialog()
	{
		// ���� ȭ���� ��ȭ ���� ������Ʈ ��Ȱ��ȭ
		SetActiveObjects(speakers[currentSpeakerIndex], false);

		// ���� ��縦 �����ϵ��� 
		currentDialogIndex ++;

		// ���� ȭ�� ���� ����
		currentSpeakerIndex = dialogs[currentDialogIndex].speakerIndex;

		// ���� ȭ���� ��ȭ ���� ������Ʈ Ȱ��ȭ
		SetActiveObjects(speakers[currentSpeakerIndex], true);
		// ���� ȭ�� �̸� �ؽ�Ʈ ����
		speakers[currentSpeakerIndex].textName.text = dialogs[currentDialogIndex].name;
		// ���� ȭ���� ��� �ؽ�Ʈ ����
		//speakers[currentSpeakerIndex].textDialogue.text = dialogs[currentDialogIndex].dialogue;
		StartCoroutine("OnTypingText");
	}

	private void SetActiveObjects(Speaker speaker, bool visible)
	{
		speaker.imageDialog.gameObject.SetActive(visible);
		speaker.textName.gameObject.SetActive(visible);
		speaker.textDialogue.gameObject.SetActive(visible);

		// ȭ��ǥ�� ��簡 ����Ǿ��� ���� Ȱ��ȭ�ϱ� ������ �׻� false
		speaker.objectArrow.SetActive(false);

		// ĳ���� ���� �� ����
		Color color = speaker.characterImage.color;
		color.a = visible == true ? 1 : 0;
		speaker.characterImage.color = color;
	}

	private IEnumerator OnTypingText()
	{
		int index = 0;
		
		isTypingEffect = true;

		// �ؽ�Ʈ�� �ѱ��ھ� Ÿ����ġ�� ���
		while ( index <= dialogs[currentDialogIndex].dialogue.Length )
		{
			speakers[currentSpeakerIndex].textDialogue.text = dialogs[currentDialogIndex].dialogue.Substring(0, index);

			index ++;
		
			yield return new WaitForSeconds(typingSpeed);
		}

		isTypingEffect = false;

		// ��簡 �Ϸ�Ǿ��� �� ��µǴ� Ŀ�� Ȱ��ȭ
		speakers[currentSpeakerIndex].objectArrow.SetActive(true);
	}

    public void EndDialogSystem()
    {
		blockImage.gameObject.SetActive(false);

		foreach (Speaker speaker in speakers)
		{
			speaker.speackerSelf.SetActive(false);
		}

		isDialogSystemEnded = true;
    }
}

[System.Serializable]
public struct Speaker
{
	public  GameObject speackerSelf;
	public	Image	characterImage;		// ĳ���� �̹��� (û��/ȭ�� ���İ� ����)
	public	Image			imageDialog;		// ��ȭâ Image UI
	public	TextMeshProUGUI	textName;			// ���� ������� ĳ���� �̸� ��� Text UI
	public	TextMeshProUGUI	textDialogue;		// ���� ��� ��� Text UI
	public	GameObject		objectArrow;		// ��簡 �Ϸ�Ǿ��� �� ��µǴ� Ŀ�� ������Ʈ
}

[System.Serializable]
public struct DialogData
{
	public	int		speakerIndex;	// �̸��� ��縦 ����� ���� DialogSystem�� speakers �迭 ����
	public	string	name;			// ĳ���� �̸�
	[TextArea(3, 5)]
	public	string	dialogue;		// ���
}

[System.Serializable]
public struct MapProfile
{
	public Sprite NPCSprite;
	public string NPCName; 
}

