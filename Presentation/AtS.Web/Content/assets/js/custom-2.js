;
(function ($) {
    $.fn.Quiz = function (options) {
        var defaults = {
            stepLenth: 1,
            correctAnswers: null,
            pasScore : 50
        }

        options = $.extend(defaults, options)

        this.each(function () {
            var $this = $(this)

            var stepsLenth = options.stepLenth;
            var successBoolToDb = true;

            // next prev
            var divs = $('.show-section section')
            var now = 0 // currently shown div
            divs.hide().first().show() // hide all divs except first

            function next() {
                divs.eq(now).hide()
                now = now + 1 < divs.length ? now + 1 : 0
                divs.eq(now).show() // show next
                console.log(now)

                showActiveStep(now)
            }
            //show active step
            function showActiveStep(now) {
                var _now = now + 1
                var stepId = '#step' + _now
                if ($(stepId).is(':visible')) {
                    $('.step-bar .bar .fill').eq(now).addClass('w-100')
                    $('#activeStep').html(_now)
                }
            }
            $('.prev').on('click', function () {
                $('.radio-field').addClass('bounce-left')
                $('.radio-field').removeClass('bounce-right')
                $('.step-bar .bar .fill').eq(now).removeClass('w-100')
                divs.eq(now).hide()
                now = now > 0 ? now - 1 : divs.length - 1
                divs.eq(now).show() // show previous
                console.log(now)

                showActiveStep(now)
            })

            // quiz validation
            var checkedradio = false

            function radiovalidate(stepnumber) {
                var checkradio = $('#step' + stepnumber + ' input')
                    .map(function () {
                        if ($(this).is(':checked')) {
                            return true
                        } else {
                            return false
                        }
                    })
                    .get()

                checkedradio = checkradio.some(Boolean)
            }

            // form validation
            $(document).ready(function () {
                var stepDivs = []
                for (var i = 1; i < stepsLenth; i++) {
                    stepDivs.push('#step' + i + 'btn')
                }
                var divQuerySelector = stepDivs.join(',')

                stepEvents = function (step, qeryElements) {
                    $(divQuerySelector).on('click', function () {
                        radiovalidate(step)

                        if (checkedradio == false) {
                            ;
                            (function (el) {
                                setTimeout(function () {
                                    el.children().remove('.reveal')
                                }, 3000)
                            })(
                                $('#error').append(
                                    '<div class="reveal alert alert-danger">Επιλέξτε μια απάντηση!</div>'
                                )
                            )

                            radiovalidate(step)
                        } else {
                            toBackEnd(step);

                            if (successBoolToDb) {
                                if (now + 1 == stepsLenth) {
                                    countresult(stepsLenth)
                                    showresult()
                                    $('#sub').html('done')
                                } else {
                                    $('#step' + step + ' .radio-field').removeClass('bounce-left')
                                    $('#step' + step + ' .radio-field').addClass('bounce-right')
                                    setTimeout(function () {
                                        next()
                                    }, 900)

                                    countresult(step)
                                }
                            }else{
                                setTimeout(function () {
                                    window.location.href = window.location.href;
                                }, 5000);
                            }
                        }
                    })
                }

                for (var i = 1; i < stepsLenth; i++) {
                    stepEvents(i, divQuerySelector)
                }

                // check last step
                $('#sub').on('click', function () {
                    radiovalidate(stepsLenth)

                    if (checkedradio == false) {
                       
                        (function (el) {
                            setTimeout(function () {
                                el.children().remove('.reveal')
                            }, 3000)
                        })(
                            $('#error').append(
                                '<div class="reveal alert alert-danger">Επιλέξτε μια απάντηση!</div>'
                            )
                        )

                        radiovalidate(stepsLenth)
                    } else {

                      toBackEnd(stepsLenth);
                        if(successBoolToDb){
                            countresult(stepsLenth)
                            showresult()
                            $('#sub').html('done')
                        }else{
                            setTimeout(function () {
                                window.location.href = window.location.href;
                            }, 5000);
                        }                       
                    }
                })

                function showresult() {
                    $('.loadingresult').css('display', 'grid')

                    setTimeout(function () {
                        $('.result_page').addClass('result_page_show')
                    }, 1000)
                }

                //correct answers
                var correct_answers = options.correctAnswers // [3, 2];

                // user answers
                let correct = 0

                var steps = $('section').length

                console.log(steps)

                let pass_check = "Δεν περάσατε";
                let result_msg = "";

                function countresult(resultnumber) {
                    
                    var correctprcnt = correct;

                    $('.u_prcnt').html(correctprcnt + '%')
                    $('.u_result span').html(correctprcnt + ' Πόντοι')

                    if (correctprcnt >= options.pasScore) {
                        
                        $('.pass_check').html('<i class="fa-solid fa-check"></i> '+pass_check+'!')
                        //$('.result_msg').html('Συγχαρητήρια! '+result_msg+'!')
                    }
                    if (now + 1 == stepsLenth){
                        GetResultMsg();
                        $('.result_msg').html(result_msg)
                    }                    
                }

                function GetResultMsg(){
                    var quizId = $("#QuizId").val();
                    $.ajax({
                        url: '/Answer/GetQuizResult',
                        type: 'POST',
                        dataType: 'json',
                        async: false,
                        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
                        data: {quizId:quizId},
                        success: function (data, text) {
                            result_msg = data.responseText;
                            return true
                        },
                        error: function (request, status, error) {
                            $('#error').append(
                                '<div class="reveal alert alert-danger">Συνέβει κάποιο σφάλμα! Παρακλούμε επικοινωνήστε με τον Administrator</div>'
                            )
                            //successBoolToDb = false;
                        }
                    })
                }

                function toBackEnd(stepNum) {
                    var steId = '#step' + stepNum
                    var valdata = $(steId).serialize()

                    $.ajax({
                        url: '/Answer/TestResult',
                        type: 'POST',
                        dataType: 'json',
                        async: false,
                        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
                        data: valdata,
                        success: function (data, text) {
                            correct += data.score;
                            if (correct >= options.pasScore){
                                pass_check = "Περάσατε";                               
                            }else{
                                pass_check = "Δεν περάσατε";
                            }
                            result_msg = data.responseText;
                            return true
                        },
                        error: function (request, status, error) {
                            $('#error').append(
                                '<div class="reveal alert alert-danger">' + request.responseJSON.responseText + '</div>'
                            )
                            successBoolToDb = false;
                        }
                    })

                }
            })
        })
    }
})(jQuery)