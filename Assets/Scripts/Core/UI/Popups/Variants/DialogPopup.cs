using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Core.UI.Popups.Variants {
	public class DialogPopup: Popup, IDisposable {
		private readonly string _text;
		private CancellationTokenSource _tokenSource;
		
		public string Text => _text;

		public DialogPopup(Transform target, string text): base(target) {
			_text = text;
		}
		public DialogPopup(Transform target, Vector3 offset, string text): base(target, offset) {
			_text = text;
		}

		public async UniTaskVoid CloseAfterAsync(float time) {
			_tokenSource = new CancellationTokenSource();
			try {
				await Awaitable.WaitForSecondsAsync(time, _tokenSource.Token);
			}
			catch (OperationCanceledException) {
				return;
			}
			finally {
				_tokenSource.Dispose();
				_tokenSource = null;
			}
			Close();
		}

		public override void Close() {
			_tokenSource?.Cancel();
			_tokenSource?.Dispose();
			_tokenSource = null;
			base.Close();
		}
		public void Dispose() {
			Close();
		}
	}
}