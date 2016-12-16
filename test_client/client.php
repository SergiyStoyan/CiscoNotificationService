<!DOCTYPE html>
<HTML>
  <HEAD>
 <meta charset="UTF-8"> 
 <link rel="icon" href="data:;base64,=">
 <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
<script type="text/javascript">
$(document).ready(function() {   
	$("form").on('submit', function(event) {     // alert($(this));
		//event.preventDefault();
		var p = $("#phone").val();
		var url = 'http://' + p;
		$(this).attr('action', url); 
    });
});
</script>

    </HEAD>
      <BODY>
         Phone: <input name="phone" id="phone" type="text" value="localhost:2222">
         <p>
         <FORM Method="POST">
<TEXTAREA NAME="XML" Rows="7" Cols="80">
<CiscoIPPhoneImageFile>
  <Title>Image Title goes here</Title>
  <Prompt>Prompt text goes here</Prompt>
  <LocationX>100</LocationX>
  <LocationY>200</LocationY>
  <URL>image1.png</URL>
</CiscoIPPhoneImageFile>
</TEXTAREA>
         <input type=submit value=POST>
       </FORM>
         <FORM Method="POST">
<TEXTAREA NAME="XML" Rows="7" Cols="80">
<CiscoIPPhoneText>
  <Title>Title text goes here</Title>
  <Prompt>The prompt text goes here</Prompt>
  <Text>The text to be displayed as the message body goes here</Text>
</CiscoIPPhoneText>
</TEXTAREA>
         <input type=submit value=POST>
       </FORM>
         <FORM Method="POST">
<TEXTAREA NAME="XML" Rows="7" Cols="80">
<CiscoIPPhoneExecute>
<ExecuteItem Priority="0" URL="Play:chime.raw"/>
</CiscoIPPhoneExecute>
</TEXTAREA>
         <input type=submit value=POST>
       </FORM>
     </BODY>
</HTML>

