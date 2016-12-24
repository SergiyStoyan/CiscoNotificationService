<!DOCTYPE html>
<HTML>
  <HEAD>
 <meta charset="UTF-8"> 
 <link rel="icon" href="data:;base64,=">
 <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
<script type="text/javascript">
$(document).ready(function() {   
	$("form").on('submit', function(event) {     // alert($(this));
		event.preventDefault();
		var p = $("#phone").val();
		var url = 'http://' + p;
		$.ajax({
			type: "POST",
			url: url,
			data: $(this).serialize(),
			success: function(data, textStatus, jqXHR){
				console.log(textStatus);
				console.log(data);
				//alert(data);
				},
			error: function(jqXHR, textStatus, errorThrown){
				console.log(textStatus);
				console.log(errorThrown);
				console.log(jqXHR);
				//alert(textStatus + " | " + errorThrown);
				},
  			//dataType: 'jsonp',
		});
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
  <Title>Emergency Lockdown</Title>
  <Prompt>Emergency Alert</Prompt>
  <Text>This is a Test of Emergency System</Text>
   <SoftKeyItem>
    <Name>Acknowledge</Name>
    <URL>http://www.testserver.com/acknowledge.jsp</URL>
    <Position>1</Position>
   </SoftKeyItem>
</CiscoIPPhoneText>

</TEXTAREA>
         <input type=submit value=POST>
       </FORM>
         <FORM Method="POST">
<TEXTAREA NAME="XML" Rows="7" Cols="80">
<CiscoIPPhoneExecute>
<ExecuteItem Priority="0" URL="Play:chime.raw"/>
<ExecuteItem Priority="0" URL="http://google.com"/>
</CiscoIPPhoneExecute>
</TEXTAREA>
         <input type=submit value=POST>
       </FORM>
     </BODY>
</HTML>

