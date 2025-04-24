import { BaumaPlugin } from 'bauma-plugin';

window.testEcho = () => {
    const inputValue = document.getElementById("echoInput").value;
    BaumaPlugin.echo({ value: inputValue })
}
