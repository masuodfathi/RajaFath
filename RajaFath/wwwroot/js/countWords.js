const textArea = document.getElementById('excerpt');
const wordCountDisplay = document.getElementById('wordCount');
const wordLimit = 35; // Change this to your desired word limit

const handleTextareaKeydown = () => {
    const text = textArea.value;
    const words = text.split(/\s+/);
    const wordCount = words.length;

    if (wordCount > wordLimit) {
        // If the word limit is exceeded, trim the text
        const trimmedText = words.slice(0, wordLimit).join(' ');
        textArea.value = trimmedText;
    }

    wordCountDisplay.textContent = wordCount + " واژه‌ها";
};

textArea.addEventListener('keydown', handleTextareaKeydown);
document.addEventListener("DOMContentLoaded", handleTextareaKeydown);