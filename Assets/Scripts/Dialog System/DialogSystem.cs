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
	private	Speaker[]		speakers;                   // 대화에 참여하는 캐릭터들의 UI 배열
    [SerializeField]
    private MapProfile[] mapProfiles;                 // 대화에 참여하는 캐릭터들의 UI 배열
    [SerializeField]
	private	DialogData[]	dialogs;                    // 현재 분기의 대사 목록 배열
	[SerializeField]
	private Image blockImage;
	public bool isDialogSystemEnded;
	[SerializeField]
	private	bool			isAutoStart = true;			// 자동 시작 여부
	private	bool			isFirst = true;				// 최초 1회만 호출하기 위한 변수
	private	int				currentDialogIndex = -1;	// 현재 대사 순번
	private	int				currentSpeakerIndex = 0;	// 현재 말을 하는 화자(Speaker)의 speakers 배열 순번
	private	float			typingSpeed = 0.1f;			// 텍스트 타이핑 효과의 재생 속도
	private	bool			isTypingEffect = false;		// 텍스트 타이핑 효과를 재생중인지

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

		// 모든 대화 관련 게임오브젝트 비활성화
		for ( int i = 0; i < speakers.Length; ++ i )
		{
			SetActiveObjects(speakers[i], false);
			// 캐릭터 이미지는 보이도록 설정
			speakers[i].characterImage.gameObject.SetActive(true);
		}
	}

	public bool UpdateDialog()
	{
		// 대사 분기가 시작될 때 1회만 호출
		if ( isFirst == true )
		{
			// 초기화. 캐릭터 이미지는 활성화하고, 대사 관련 UI는 모두 비활성화
			Setup();

			// 자동 재생(isAutoStart=true)으로 설정되어 있으면 첫 번째 대사 재생
			if ( isAutoStart ) SetNextDialog();

			isFirst = false;
		}

		if ( Input.GetMouseButtonDown(0) )
		{
			// 텍스트 타이핑 효과를 재생중일때 마우스 왼쪽 클릭하면 타이핑 효과 종료
			if ( isTypingEffect == true )
			{
				isTypingEffect = false;
				
				// 타이핑 효과를 중지하고, 현재 대사 전체를 출력한다
				StopCoroutine("OnTypingText");
				speakers[currentSpeakerIndex].textDialogue.text = dialogs[currentDialogIndex].dialogue;
				// 대사가 완료되었을 때 출력되는 커서 활성화
				speakers[currentSpeakerIndex].objectArrow.SetActive(true);

				return false;
			}

			// 대사가 남아있을 경우 다음 대사 진행
			if ( dialogs.Length > currentDialogIndex + 1 )
			{
				SetNextDialog();
			}
			// 대사가 더 이상 없을 경우 모든 오브젝트를 비활성화하고 true 반환
			else
			{
				// 현재 대화에 참여했던 모든 캐릭터, 대화 관련 UI를 보이지 않게 비활성화
				for ( int i = 0; i < speakers.Length; ++ i )
				{
					SetActiveObjects(speakers[i], false);
					// SetActiveObjects()에 캐릭터 이미지를 보이지 않게 하는 부분이 없기 때문에 별도로 호출
					speakers[i].characterImage.gameObject.SetActive(false);
				}

				return true;
			}
		}

		return false;
	}

	private void SetNextDialog()
	{
		// 이전 화자의 대화 관련 오브젝트 비활성화
		SetActiveObjects(speakers[currentSpeakerIndex], false);

		// 다음 대사를 진행하도록 
		currentDialogIndex ++;

		// 현재 화자 순번 설정
		currentSpeakerIndex = dialogs[currentDialogIndex].speakerIndex;

		// 현재 화자의 대화 관련 오브젝트 활성화
		SetActiveObjects(speakers[currentSpeakerIndex], true);
		// 현재 화자 이름 텍스트 설정
		speakers[currentSpeakerIndex].textName.text = dialogs[currentDialogIndex].name;
		// 현재 화자의 대사 텍스트 설정
		//speakers[currentSpeakerIndex].textDialogue.text = dialogs[currentDialogIndex].dialogue;
		StartCoroutine("OnTypingText");
	}

	private void SetActiveObjects(Speaker speaker, bool visible)
	{
		speaker.imageDialog.gameObject.SetActive(visible);
		speaker.textName.gameObject.SetActive(visible);
		speaker.textDialogue.gameObject.SetActive(visible);

		// 화살표는 대사가 종료되었을 때만 활성화하기 때문에 항상 false
		speaker.objectArrow.SetActive(false);

		// 캐릭터 알파 값 변경
		Color color = speaker.characterImage.color;
		color.a = visible == true ? 1 : 0;
		speaker.characterImage.color = color;
	}

	private IEnumerator OnTypingText()
	{
		int index = 0;
		
		isTypingEffect = true;

		// 텍스트를 한글자씩 타이핑치듯 재생
		while ( index <= dialogs[currentDialogIndex].dialogue.Length )
		{
			speakers[currentSpeakerIndex].textDialogue.text = dialogs[currentDialogIndex].dialogue.Substring(0, index);

			index ++;
		
			yield return new WaitForSeconds(typingSpeed);
		}

		isTypingEffect = false;

		// 대사가 완료되었을 때 출력되는 커서 활성화
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
	public	Image	characterImage;		// 캐릭터 이미지 (청자/화자 알파값 제어)
	public	Image			imageDialog;		// 대화창 Image UI
	public	TextMeshProUGUI	textName;			// 현재 대사중인 캐릭터 이름 출력 Text UI
	public	TextMeshProUGUI	textDialogue;		// 현재 대사 출력 Text UI
	public	GameObject		objectArrow;		// 대사가 완료되었을 때 출력되는 커서 오브젝트
}

[System.Serializable]
public struct DialogData
{
	public	int		speakerIndex;	// 이름과 대사를 출력할 현재 DialogSystem의 speakers 배열 순번
	public	string	name;			// 캐릭터 이름
	[TextArea(3, 5)]
	public	string	dialogue;		// 대사
}

[System.Serializable]
public struct MapProfile
{
	public Sprite NPCSprite;
	public string NPCName; 
}

