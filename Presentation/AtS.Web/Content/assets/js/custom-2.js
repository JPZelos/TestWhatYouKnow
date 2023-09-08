var stepsLenth = 2;
// next prev
var divs = $('.show-section section');
var now = 0; // currently shown div
divs.hide().first().show(); // hide all divs except first

function next() {
    divs.eq(now).hide();
    now = (now + 1 < divs.length) ? now + 1 : 0;
    divs.eq(now).show(); // show next
    console.log(now);

    showActiveStep(now);
}
//show active step
function showActiveStep(now) {
    var _now = now + 1;
    var stepId = "#step" + _now;
    if ($(stepId).is(':visible')) {
        $(".step-bar .bar .fill").eq(now).addClass('w-100');
        $("#activeStep").html(_now);
    }
}
$(".prev").on('click', function () {

    $('.radio-field').addClass('bounce-left');
    $('.radio-field').removeClass('bounce-right');
    $(".step-bar .bar .fill").eq(now).removeClass('w-100');
    divs.eq(now).hide();
    now = (now > 0) ? now - 1 : divs.length - 1;
    divs.eq(now).show(); // show previous
    console.log(now);

    showActiveStep(now);
})

// quiz validation
var checkedradio = false;

function radiovalidate(stepnumber) {
    var checkradio = $("#step" + stepnumber + " input").map(function () {
        if ($(this).is(':checked')) {
            return true;
        } else {
            return false;
        }
    }).get();

    checkedradio = checkradio.some(Boolean);
}

// form validation
$(document).ready(function () {
    var stepDivs = [];
    for (var i = 1; i < stepsLenth; i++) {
        stepDivs.push("#step"+i+"btn");
     }
     var divQuerySelector = stepDivs.join(",");

     stepEvents = function(step, qeryElements){
        $(divQuerySelector).on('click', function () {
            radiovalidate(step);

            if (checkedradio == false) {

                (function (el) {
                    setTimeout(function () {
                        el.children().remove('.reveal');
                    }, 3000);
                }($('#error').append('<div class="reveal alert alert-danger">Choose an option!</div>')));

                radiovalidate(step);

            } else {
                toBackEnd(step);

                if ((now + 1) == stepsLenth) {
                    countresult(stepsLenth);
                    showresult();
                    $("#sub").html('done');
                } else {
                    $('#step' + step + ' .radio-field').removeClass('bounce-left');
                    $('#step' + step + ' .radio-field').addClass('bounce-right');
                    setTimeout(function () {
                        next();
                    }, 900)

                    countresult(step);
                }
            }
        })
     } 

     for(var i=1; i< stepsLenth; i++){
        stepEvents(i,divQuerySelector);
     }
       
    // check last step
    $("#sub").on('click', function () {
        radiovalidate(stepsLenth);

        if (checkedradio == false) {

            (function (el) {
                setTimeout(function () {
                    el.children().remove('.reveal');
                }, 3000);
            }($('#error').append('<div class="reveal alert alert-danger">Choose an option!</div>')));

            radiovalidate(stepsLenth);

        } else {
            toBackEnd(stepsLenth);

            countresult(stepsLenth);
            showresult();
            $("#sub").html('done');

        }
    })

    function showresult() {
        $('.loadingresult').css('display', 'grid');

        setTimeout(function () {
            $('.result_page').addClass('result_page_show');
        }, 1000)
    };

    //correct answers
    var correct_answers = [3, 2];

    // user answers
    let correct = 0;

    var steps = $('section').length;

    console.log(steps);

    function countresult(resultnumber) {

        $('#step' + resultnumber + ' .radio-field input').each(function () {

            //Here we can use Ajax to check the correct answer
            for (var i = 0; i <= correct_answers.length; i++) {

                if ($(this).is(':checked')) {

                    if ($(this).val() == correct_answers[i]) {
                        correct++
                        break
                    }
                }
            }
        })

        var correctprcnt = (correct / steps) * 100

        $('.u_prcnt').html(correctprcnt + '%')
        $('.u_result span').html(correctprcnt + ' Points')

        if (correctprcnt >= 80) {
            $('.pass_check').html('<i class="fa-solid fa-check"></i> You Passed!')
            $('.result_msg').html('You passed the test!')
        }
    };

    function toBackEnd(stepNum) {
        var steId = "#step" + stepNum;
        var valdata = $(steId).serialize();

        //alert(valdata);
        $.ajax({
            url: "/Answer/TestResult",
            type: "POST",
            dataType: 'json',
            contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
            data: valdata
        });
    };

})