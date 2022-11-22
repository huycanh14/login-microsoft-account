part of 'base_input_cubit.dart';

abstract class BaseInputState extends Equatable {
  final ValidatorStatus statusValid;
  const BaseInputState(this.statusValid);

  @override
  List<Object> get props => [statusValid];
}

class BaseInputInitial extends BaseInputState {
  const BaseInputInitial(super.statusValid);
}

class BaseInputUpdate extends BaseInputState {
  const BaseInputUpdate(super.statusValid);
}
