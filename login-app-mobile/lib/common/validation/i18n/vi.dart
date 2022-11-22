import 'package:intl/intl.dart';

import '../form_validator.dart';

class LocaleVi implements FormValidator {
  const LocaleVi();

  @override
  String name() => 'vi';

  @override
  String minLength(String v, int n) => 'Phải chứa ít nhất $n ký tự';

  @override
  String maxLength(String v, int n) => 'Phải chứa nhiều nhất $n ký tự';

  @override
  String email(String v) => 'Không đúng định dạng email';

  @override
  String phoneNumber(String v) => 'Không đúng định dạng số điện thoại';

  @override
  String required() => 'Không được bỏ trống';

  @override
  String ip(String v) => 'Không phải là địa chỉ IP';

  @override
  String ipv6(String v) => 'Không phải là địa chỉ IPv6';

  @override
  String url(String v) => 'Không phải là địa chỉ url';

  @override
  String after(String v, [DateTime? date]) {
    date ??= DateTime.now();
    return "Giá trị không sau ngày ${DateFormat('dd/MM/yyyy').format(date)}";
  }

  @override
  String alpha(String v) => "Chỉ có thể chứa các kí tự chữ";

  @override
  String alphanumeric(String v) => "Chỉ có thể chứa các kí tự chữ và số";

  @override
  String ascii(String v) => "Không phải là mã ASCII";

  @override
  String base64(String v) => "Không phải là Base64";

  @override
  String before(String v, [DateTime? date]) {
    date ??= DateTime.now();
    return "Giá trị không trước ngày ${DateFormat('dd/MM/yyyy').format(date)}";
  }

  @override
  String byteLength(String v, int n) => "Độ dài byte phải bằng $n";

  @override
  String creditCard(String v) => "Không phải là credit card";

  @override
  String date(String v) => "Không phải là dạng ngày";

  @override
  String divisibleBy(String v, int n) => "Không chia hết cho $n";

  @override
  String fqdn(String v) => "Không phải là tên miền";

  @override
  String fullWidth(String v) => "Không có độ rộng ký tự phù hợp";

  @override
  String halfWidth(String v) => "Không có độ rộng ký tự phù hợp";

  @override
  String hexColor(String v) => "Không phải là mã Hex Color";

  @override
  String hexadecimal(String v) => "Không phải là mã Hexa decimal";

  @override
  String isFloat(String v) => "Không phải là kiểu dữ liệu số thực";
  @override
  String isInt(String v) => "Không phải là kiểu dữ liệu số nguyên";

  @override
  String isNull(String v) => "Dữ liệu đang trống";

  @override
  String isbn(String v) => "Không phải là mã code ISBN";

  @override
  String json(String v) => "Không phải là kiểu dữ liệu Json";

  @override
  String length(String v, int n) => "Phải có độ dài bằng $n";

  @override
  String lowercase(String v) => "Các ký tự phải viết thường";

  @override
  String mongoId(String v) => "Không phải là mongoId";

  @override
  String multibyte(String v) => "Không chưa ký tự nhiều byte";

  @override
  String numeric(String v) => "Không phải dạng số";

  @override
  String postalCode(String v) => "Không phải Postal Code";

  @override
  String surrogatePair(String v) => "Không có chứa ký tự thay thế";

  @override
  String uppercase(String v) => "Các ký tự phải viết hoa";

  @override
  String uuid(String v) => "Không phải mã uuid";

  @override
  String variableWidth(String v) => "Chuỗi khoong có chứa hỗn hợp các ký tự đầy đủ và nửa chiều rộng không";
}
