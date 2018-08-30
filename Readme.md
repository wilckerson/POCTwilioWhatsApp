## Objetivo
Desenvolver um chat onde as mensagens enviadas e recebidas são feitas via WhatsApp com a API de integração do Twilio

Referências:
- [Documentação Twilio WhatsApp](https://www.twilio.com/docs/sms/whatsapp/api)

## Arquitetura

**WhatsApp Template Message:** O chat pode utilizar mensagens fixas para iniciar uma conversa com um cliente

```
ChatClientUI -> (escolher mensagem fixa/template) -> ChatAPI -> (salva mensagem no BD) -> TwilioAPI -> WhatsAppBusiness -> (notificar mensagem no whatsApp) -> Cliente
```

**WhatsApp Session Message:** Quando o cliente iniciou a conversa ou respondeu ao WhatsApp Template Message

```
Cliente -> (envia mensagem via WhatsApp) -> WhatsAppBusiness -> TwilioAPI -> (WebHook) -> ChatAPI -> (salva mensagem no BD) -> SignalR -> (notificar mensagem) -> ChatClientUI
```

```
ChatClientUI -> (enviar mensagem) -> ChatAPI -> (salva mensagem no BD) -> TwilioAPI -> WhatsAppBusiness -> (notificar mensagem no whatsApp) -> Cliente
```

## ChatClientUI
- O chat possui uma lista de contatos
- Ao clicar num contato, será iniciado a tela do chat
- Nesse momento deve-se buscar as mensagens anteriores no BD e exibir na tela, se houver
- Verificar se está numa WhatsApp Session Message ou Template Message
- Se tiver na Session Message pode responder normalmente
- Se tiver na Template Message precisa escolher dentre as mensagens fixas

## Tarefas
- [x] Conectar um celular no TwilioSandBox
- [x] Enviar uma mensagem simples para o celular conectado (TemplateMessage)
- [ ] Criar um EndPoint público para receber as chamadas de WebHook do Twilio
- [ ] Configurar a Url do EndPoint no plataforma do Twilio
- [ ] Utilizar o celular conectado para responder uma mensagem e verificar se chegou via WebHook
- [ ] Enviar uma mensagem para o celular conectado (SessionMessage)
- [ ] Descrever outras tarefas relacionadas ao SignalR, DB e ChatClientUI
