import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';

import '../../../constant/app_color.dart';
import '../../../validation/check_validor.dart';
import '../../../validation/form_validation_manager.dart';
import '../../../validation/validtor_status.dart';
import 'cubit/base_input_cubit.dart';

class BaseInputText extends StatefulWidget {
  final String? valueDefault;
  final bool readOnly;
  final String title;
  final String? rules;
  final String? hintText;
  final TextEditingController? controller;
  final Color baseColor;
  final Color borderColor;
  final Color errorColor;
  final Color successColor;
  final Color borderFocus;
  final TextInputType inputType;
  final bool obscureText;
  final Function? validator;
  final Function onChanged;
  final Widget? prefixIcon;
  final Widget? suffixIcon;
  final bool focus;
  final String id;
  final FormValidationManager? formValidationManager;
  const BaseInputText({
    Key? key,
    this.readOnly = false,
    this.baseColor = AppColor.grey,
    this.borderColor = AppColor.borderGrey,
    this.errorColor = AppColor.error,
    this.successColor = AppColor.success,
    this.borderFocus = AppColor.focus,
    this.inputType = TextInputType.text,
    this.obscureText = false,
    this.focus = false,
    this.valueDefault,
    this.rules,
    this.hintText,
    this.controller,
    this.validator,
    this.prefixIcon,
    this.suffixIcon,
    this.formValidationManager,
    required this.title,
    required this.onChanged,
    required this.id,
  }) : super(key: key);

  @override
  State<BaseInputText> createState() => _BaseInputTextState();
}

class _BaseInputTextState extends State<BaseInputText> {
  late String? Function(String?) validator;
  late final TextEditingController controller;

  @override
  void initState() {
    super.initState();
    if (widget.rules != null && widget.rules != "") {
      validator = checkValidation(widget.rules!);
    } else if (widget.validator != null) {
      validator = (text) {
        return widget.validator!(text);
      };
    } else {
      validator = (text) {
        return null;
      };
    }
    if (widget.controller != null) {
      widget.controller!.text = widget.valueDefault ?? "";
      controller = widget.controller!;
    } else {
      controller = TextEditingController(text: widget.valueDefault ?? "");
    }
  }

  @override
  Widget build(BuildContext context) {
    return BlocProvider(
      create: (context) => BaseInputCubit(),
      child: BlocBuilder<BaseInputCubit, BaseInputState>(
        builder: (context, state) {
          return Padding(
            padding: const EdgeInsets.symmetric(horizontal: 10.0, vertical: 8.0),
            child: Column(
              mainAxisAlignment: MainAxisAlignment.start,
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                Padding(
                  padding: const EdgeInsets.symmetric(horizontal: 8.0, vertical: 4.0),
                  child: RichText(
                    text: TextSpan(
                      text: "${widget.title} ",
                      style: TextStyle(
                        color: state.statusValid == ValidatorStatus.error ? widget.errorColor : AppColor.textDefault,
                      ),
                      children: <TextSpan>[
                        TextSpan(
                          text: widget.rules != null && widget.rules!.isNotEmpty ? "(*)" : "",
                        ),
                        const TextSpan(text: ':'),
                      ],
                    ),
                  ),
                ),
                TextFormField(
                  obscureText: widget.obscureText,
                  readOnly: widget.readOnly,
                  autovalidateMode: AutovalidateMode.onUserInteraction,
                  style: TextStyle(
                    color: widget.readOnly ? AppColor.grey : null,
                  ),
                  validator: (text) {
                    String? _res;
                    if (widget.formValidationManager != null) {
                      _res = widget.formValidationManager?.wrapValidatorAsString(widget.id, validator, text);
                    } else {
                      _res = validator(text);
                    }
                    if (_res != null && _res != "") {
                      context.read<BaseInputCubit>().updateState(ValidatorStatus.error);
                    } else {
                      context.read<BaseInputCubit>().updateState(ValidatorStatus.passed);
                    }
                    return _res;
                  },
                  focusNode: widget.formValidationManager != null
                      ? widget.formValidationManager!.getFocusNodeForField(widget.id)
                      : null,
                  keyboardType: widget.inputType,
                  controller: controller,
                  decoration: InputDecoration(
                    prefixIcon: widget.prefixIcon,
                    suffixIcon: widget.readOnly
                        ? const Icon(Icons.edit_off, color: AppColor.disable)
                        : widget.suffixIcon ??
                            (() {
                              switch (state.statusValid) {
                                case ValidatorStatus.error:
                                  return Icon(Icons.error, color: widget.errorColor);
                                case ValidatorStatus.passed:
                                  return Icon(Icons.check, color: widget.successColor);
                                default:
                                  return Icon(Icons.info, color: widget.borderColor);
                              }
                            }()),
                    filled: true,
                    fillColor: Colors.white,
                    focusedBorder: OutlineInputBorder(
                      borderRadius: BorderRadius.circular(15.0),
                      borderSide: BorderSide(
                        width: 1,
                        color: widget.readOnly ? AppColor.disable : widget.borderFocus,
                      ),
                    ),
                    disabledBorder: OutlineInputBorder(
                      borderRadius: BorderRadius.circular(15.0),
                      borderSide: const BorderSide(
                        width: 1,
                        color: AppColor.disable,
                      ),
                    ),
                    enabledBorder: OutlineInputBorder(
                      borderRadius: BorderRadius.circular(15.0),
                      borderSide: BorderSide(
                        width: 1,
                        color: (() {
                          switch (state.statusValid) {
                            case ValidatorStatus.passed:
                              return widget.successColor;
                            default:
                              return widget.borderColor;
                          }
                        }()),
                      ),
                    ),
                    border: OutlineInputBorder(
                      borderRadius: BorderRadius.circular(15.0),
                      borderSide: BorderSide(
                        width: 1,
                        color: (() {
                          switch (state.statusValid) {
                            case ValidatorStatus.passed:
                              return widget.successColor;
                            default:
                              return widget.borderColor;
                          }
                        }()),
                      ),
                    ),
                    errorBorder: OutlineInputBorder(
                        borderRadius: BorderRadius.circular(15.0),
                        borderSide: BorderSide(
                          width: 1,
                          color: widget.errorColor,
                        )),
                    focusedErrorBorder: OutlineInputBorder(
                      borderRadius: BorderRadius.circular(15.0),
                      borderSide: BorderSide(
                        width: 1,
                        color: widget.errorColor,
                      ),
                    ),
                    hintText: widget.hintText,
                  ),
                  autofocus: widget.focus,
                  onChanged: (text) => widget.onChanged(text),
                ),
              ],
            ),
          );
        },
      ),
    );
  }
}
