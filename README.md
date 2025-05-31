![Ollama](https://img.shields.io/badge/Ollama-%23000000?logo=Ollama)
![Unity](https://img.shields.io/badge/Unity-unity?logo=Unity&color=%23000000)
![C#](https://img.shields.io/badge/C%23-%23512BD4?logo=.NET)
![License](https://img.shields.io/github/license/HardCodeDev777/SimpleOllamaUnity?color=%2305991d)
![Last commit](https://img.shields.io/github/last-commit/HardCodeDev777/SimpleOllamaUnity?color=%2305991d)
![Tag](https://img.shields.io/github/v/tag/HardCodeDev777/SimpleOllamaUnity)
![Top lang](https://img.shields.io/github/languages/top/HardCodeDev777/SimpleOllamaUnity)

# 🦙 SimpleOllamaUnity — Unity Extension

Listed in [Ollama Community Integrations](https://github.com/ollama/ollama?tab=readme-ov-file#community-integrations)

&nbsp;

> Communicate with local LLMs in Unity using [Ollama](https://ollama.com) — in just **two lines of code**.

---

## 🚀 Overview

**SimpleOllamaUnity** is an Unity extension that lets you communicate with Ollama in just two lines of code!
It also works at runtime, so you can use it in your games!

You can easily configure the following for a quick start:
- 🤖 Model
- 📃 System prompt
- 🌐 Ollama URI
- 👀 Reasoning (optional — can be disabled)

---

## 📦 Installation

1. Download the latest .unitypackage file from the Releases page.
2. Drag & drop it into your Unity project window.
3. Unity will automatically compile the editor extension.

No additional setup required.

The Plugins folder includes several .dll files required for integration.

---

## 💻 Usage

```csharp
var ollama = new Ollama(new OllamaConfig(
    modelName: "qwen2.5:3b",
    systemPrompt: "Your answer mustn't be more than 10 words"
    ));

var response = await ollama.SendMessage(new OllamaRequest(
    userPrompt: "When was GitHub created?"
));
```

Yes, that’s it — only **two lines of code**! 🎉

&nbsp;

To use a custom server URI:

```csharp
var ollama = new Ollama(new OllamaConfig(
    modelName: "qwen2.5:3b",
    systemPrompt: "Your answer mustn't be more than 10 words",
    uri: "http://my-custom-server.local:3000/api/process"
)); 
```

&nbsp;

You can also remove reasoning from models that can do it:

```csharp
var response = await ollama.SendMessage(new OllamaRequest(
    userPrompt: "When was GitHub created?",
    clearThinking: true
));
```

This will remove all reasoning (from `<think>` to `</think>`).

&nbsp;
&nbsp;

### 🧪 Full Example:

```csharp
using UnityEngine;
using HardCodeDev.SimpleOllamaUnity;

public class Test : MonoBehaviour
{
    private async void Start()
    {
        var ollama = new OllamaBase(new OllamaConfig(
            modelName: "qwen2.5:3b",
            systemPrompt: "Your answer mustn't be more than 10 words"
        ));

        var response = await ollama.SendMessage(new OllamaRequest(
            userPrompt: "When was GitHub created?"
        ));

        Debug.Log(response); // Prints LLM response to the console
    }
}
```

---

## 🛠 TODO

- [ ] Review which `.dll` files in the `Plugins` folder are actually required and remove the unnecessary ones.

---

## 📄 License

This project is licensed under the **MIT License**.  
See the [LICENSE](LICENSE) file for full terms.

---

## 👨‍💻 Author

**HardCodeDev**  
- [GitHub](https://github.com/HardCodeDev777)  
- [Itch.io](https://hardcodedev.itch.io/)

---

> 💬 Got feedback, found a bug, or want to contribute? Open an issue or fork the repo on GitHub!
