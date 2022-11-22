import 'package:equatable/equatable.dart';
import 'package:flutter_bloc/flutter_bloc.dart';

import '../../../../validation/validtor_status.dart';

part 'base_input_state.dart';

class BaseInputCubit extends Cubit<BaseInputState> {
  BaseInputCubit() : super(const BaseInputInitial(ValidatorStatus.notChecked));

  void updateState(ValidatorStatus status) {
    emit(BaseInputUpdate(status));
  }
}
