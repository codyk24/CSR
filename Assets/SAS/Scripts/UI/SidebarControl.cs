using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using DG.Tweening.Core;
using Options = DG.Tweening.Plugins.Options;

namespace Titan.UI
{
	public class SidebarControl : MonoBehaviour
	{
		#region Fields

		[SerializeField]
		private RectTransform m_sidebar;

		[SerializeField]
		private RectTransform m_document;

		[SerializeField]
		private float m_duration = 0.2f;

		[SerializeField]
		private AnimationCurve m_curve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

		[SerializeField]
		private Toggle m_toggle;

		private TweenerCore<float, float, Options.FloatOptions> m_tweener;
		private float m_value;

		#endregion

		#region Properties

		public Toggle toggle => m_toggle;

		protected float sidebarWidth => m_sidebar.rect.width;

		#endregion

		#region Methods

		private void OnEnable()
		{
			m_toggle.onValueChanged.AddListener(ValueChanged);
			Process(1f);
		}

		private void OnDisable()
		{
			m_toggle.onValueChanged.RemoveListener(ValueChanged);
		}

		private void ValueChanged(bool isOn)
		{
			if (m_tweener != null && m_tweener.IsPlaying())
			{
				m_tweener.Kill();
			}

			m_value = 0f;
			m_tweener = DOTween.To(() => m_value, Process, 1f, m_duration)
				.SetEase(m_curve);
		}

		private void Process(float value)
		{
			if (m_toggle.isOn)
			{
				m_sidebar.anchoredPosition = new Vector2(
					Mathf.Lerp(sidebarWidth, 0f, value),
					m_sidebar.anchoredPosition.y);

				m_document.offsetMax = new Vector2(
					Mathf.Lerp(0f, -sidebarWidth, value),
					m_document.offsetMax.y);
			}
			else
			{
				m_sidebar.anchoredPosition = new Vector2(
					Mathf.Lerp(0f, sidebarWidth, value),
					m_sidebar.anchoredPosition.y);

				m_document.offsetMax = new Vector2(
					Mathf.Lerp(-sidebarWidth, 0f, value),
					m_document.offsetMax.y);
			}

			m_value = value;
		}

		public void Hide(bool blah)
		{
			Debug.LogFormat("DEBUG... REached {0}", blah);
		}

		#endregion
	}
}
