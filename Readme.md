# ITCC Yandex SpeechKit client

## General

### Capabilities

* Speech recognition ([HTTP mode](https://tech.yandex.com/speechkit/cloud/doc/guide/concepts/asr-http-request-docpage/) / [Data Streaming mode](https://tech.yandex.com/speechkit/cloud/doc/guide/concepts/asr-protobuf-docpage))  
* Speech synthesis  

## Usage

### Speech recognition

Use HTTP mode for simple speech-to-text conversion if you want just convert one audio file to text without intermediate results or analytics:

```


```

If you want to receive intermediate result during recognition or advanced information (skeaker age group, detected language, recognition confidence of each word in phrase etc.) - use data streaming mode.

```

```

### Speech synthesis